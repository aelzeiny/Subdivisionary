using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Subdivisionary.Models;

namespace Subdivisionary.Controllers.Api
{
    /// <summary>
    /// API that has everything to do with Form Data
    /// </summary>
    [Authorize(Roles = EUserRoles.Admin)]
    public class FormsController : ApiController
    {
        /// <summary>
        /// Application Context is the portal between the SQL database and the model.
        /// It is the single greatest thing about Entity Framework.
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Init ApplicationDB Context for serialization
        /// </summary>
        public FormsController()
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
        /// List All Ids of all forms within an application,
        /// as designated by Application Id
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<int> ListIds(int id)
        {
            var app = _context.Applications.Where(x => x.Id == id).Include(x => x.Forms).FirstOrDefault();
            if (app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return app.Forms.Select(x => x.Id);
        }
    }
}
