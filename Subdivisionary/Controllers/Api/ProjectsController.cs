using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Subdivisionary.DAL;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Controllers.Api
{
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        /**
         * Returns all applications
         * For example, this responds to a GET request like:
         * GET /api/applications/1
         */
        public IEnumerable<BasicProjectInfoDto> GetApplications()
        {
            return _context.ProjectInfos.ToList().Select(Mapper.Map<BasicProjectInfo, BasicProjectInfoDto>);
        }

        /**
         * Returns an application with a given application id
         * For example, this responds to a GET request like:
         * GET /api/applications/1
         */
        public BasicProjectInfoDto GetApplication(int id)
        {
            var projectinfo = _context.ProjectInfos.SingleOrDefault(a => a.Id == id);

            if (projectinfo == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<BasicProjectInfo, BasicProjectInfoDto>(projectinfo);
        }

        /**
         * Adds an application to the given collection
         * For example, this responds to a POST request like:
         * POST /api/applications
         */
        [HttpPost]
        public BasicProjectInfoDto CreateApplication(BasicProjectInfoDto infoDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var info = Mapper.Map<BasicProjectInfoDto, BasicProjectInfo>(infoDto);

            _context.ProjectInfos.Add(info);
            _context.SaveChanges();

            infoDto.Id = info.Id;

            return infoDto;
        }


        /**
         * Updates an application in the given collection
         * For example, this responds to a PUT request like:
         * PUT /api/applications/1
         */
        [HttpPut]
        public void UpdateApplication(int id, BasicProjectInfoDto infoDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var infoInDb = _context.ProjectInfos.SingleOrDefault(a => a.Id == id);

            if (infoInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(infoDto, infoInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteApplication(int id)
        {
            var infoInDb = _context.ProjectInfos.SingleOrDefault(a => a.Id == id);

            if (infoInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.ProjectInfos.Remove(infoInDb);
            _context.SaveChanges();
        }
    }
}
