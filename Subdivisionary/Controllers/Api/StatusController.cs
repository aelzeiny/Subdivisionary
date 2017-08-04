using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Subdivisionary.Dtos;
using Subdivisionary.Models;

namespace Subdivisionary.Controllers.Api
{
    /// <summary>
    /// Status Controller is used to retrieve and/or modify the status of the project.
    /// For security/debugging purposes, there is no such thing as modifying the 
    /// status of a project directly.
    /// </summary>
    [Authorize(Roles = EUserRoles.Admin)]
    public class StatusController : ApiController
    {
        /// <summary>
        /// Application Context is the portal between the SQL database and the model.
        /// It is the single greatest thing about Entity Framework.
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Init ApplicationDB Context for serialization
        /// </summary>
        public StatusController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Disposed of the ApplicationDbContext instance to free-up server resources.
        /// </summary>
        /// <param name="disposing">isDisposing bool</param>
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /// <summary>
        /// Gets the current status by Application ID. Note: Not Status ID
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>Application Status Log Item, which includes the current status, date, and string for additional data</returns>
        [HttpGet]
        public ApplicationStatusLogItemDtoGet Status(int id)
        {
            var app = _context.Applications.Where(x => x.Id == id).Include(x => x.StatusHistory).FirstOrDefault();
            if(app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<ApplicationStatusLogItemDtoGet>(app.CurrentStatusLog);
        }

        /// <summary>
        /// This manual override sets the parameter for application status, but does 
        /// not trigger the subsequent effects. 
        /// (i.e: generating invoices, marking an application as editable, etc.)
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <param name="status">status to be set as the current application status.</param>
        [HttpPost]
        public void AddStatus(int id, ApplicationStatusLogItemDtoSet status)
        {
            var app = _context.Applications.Where(x => x.Id == id).Include(x => x.StatusHistory).FirstOrDefault();
            if (app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            app.StatusHistory.Add(Mapper.Map<ApplicationStatusLogItem>(status));
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets History for Application, as given by application ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ICollection<ApplicationStatusLogItem> History(int id)
        {
            var app = _context.Applications.Where(x => x.Id == id).Include(x => x.StatusHistory).FirstOrDefault();
            if (app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return app.StatusHistory;
        }

        /// <summary>
        /// Modify status by designated Status ID. Note: Not Application Id.
        /// </summary>
        /// <param name="id">STATUS ID</param>
        /// <param name="status">Status to be set</param>
        [HttpPut]
        public void Status(int id, ApplicationStatusLogItemDtoSet status)
        {
            var currStat = _context.StatusHistory.Find(id);
            Mapper.Map(status, currStat);
            _context.SaveChanges();
        }

        /// <summary>
        /// Check to see if an application has a given status, as designated by App-ID
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <param name="status">Value of status that is being checked for</param>
        /// <returns>bool that designates whether an application has a given status</returns>
        [HttpGet]
        public bool HasStatus(int id, EApplicationStatus status)
        {

            var app = _context.Applications.Where(x => x.Id == id).Include(x => x.StatusHistory).FirstOrDefault();
            if (app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return app.HasStatus(status);
        }
    }
}
