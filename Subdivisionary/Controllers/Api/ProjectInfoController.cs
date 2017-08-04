using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Subdivisionary.DAL;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;
using Subdivisionary.ViewModels.ApplicationViewModels;

namespace Subdivisionary.Controllers.Api
{
    /// <summary>
    /// API Controller specifically for the management of Project Information Forms.
    /// Not Intended for the management of standard forms. Not Intended for the management of Projects.
    /// Note that there are several types of Project Information sub-classes, which come into play depending
    /// on the type of application is being used. See the ApplicationTypeToProjectInfo().
    /// </summary>
    [Authorize(Roles = EUserRoles.Admin)]
    public class ProjectInfoController : ApiController
    {

        /// <summary>
        /// Application Context is the portal between the SQL database and the model.
        /// It is the single greatest thing about Entity Framework.
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Init ApplicationDB Context for serialization
        /// </summary>
        public ProjectInfoController()
        {
            _context = new ApplicationDbContext();
        }
        /// <summary>
        /// Returns all applications
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BasicProjectInfoDto> ProjectInfos()
        {
            return _context.ProjectInfos.ToList().Select(Mapper.Map<BasicProjectInfo, BasicProjectInfoDto>);
        }

        /// <summary>
        /// Returns the project information for  with a given project info form id
        /// </summary>
        /// <param name="id">Project Info Form Id to be queried in DB</param>
        /// <returns>Project Info for given project info id</returns>
        public BasicProjectInfoDto ProjectInfo(int id)
        {
            var projectinfo = _context.ProjectInfos.FirstOrDefault(a => a.Id == id);

            if (projectinfo == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<BasicProjectInfo, BasicProjectInfoDto>(projectinfo);
        }
        /// <summary>
        /// Returns the project information for  with a given application id
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <returns></returns>
        public BasicProjectInfoDto ProjectInfoByApplicationId(int appId)
        {
            var app = _context.Applications.Where(a => a.Id == appId).Include(x=>x.ProjectInfo).FirstOrDefault();

            if (app == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<BasicProjectInfo, BasicProjectInfoDto>(app.ProjectInfo);
        }

        /// <summary>
        /// Updates the project info given by a specified project info ID.
        /// Note that certain project types have certain Project Information form
        /// requirements.
        /// </summary>
        [HttpPut]
        public void ProjectInfo(int id, BasicProjectInfoDto infoDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var infoInDb = _context.ProjectInfos.SingleOrDefault(a => a.Id == id);

            if (infoInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(infoDto, infoInDb);

            _context.SaveChanges();
        }

        /// <summary>
        /// Returns Project Information object based on the Application Type 
        /// </summary>
        /// <param name="appType">Supported Application Type</param>
        /// <returns></returns>
        [HttpGet]
        public BasicProjectInfoDto ApplicationTypeToProjectInfo(EApplicationTypeViewModel appType)
        {
            throw new NotImplementedException("TODO: Implement Some other time, once Application controller is done.");
        }

        /// <summary>
        /// Disposed of the ApplicationDbContext instance to free-up server resources.
        /// </summary>
        /// <param name="disposing">isDisposing bool</param>
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}