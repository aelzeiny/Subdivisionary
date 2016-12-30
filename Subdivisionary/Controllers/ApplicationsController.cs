using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Subdivisionary.DAL;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;

namespace Subdivisionary.Controllers
{

    public class ApplicationsController : Controller
    {
        private ApplicationDbContext _context;

        public ApplicationsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Applications
        public ViewResult Index()
        {
            // Get Applications From Database
            var user = GetCurrentUser();
            var applicant = _context.Applicants
                .Include(app => app.Applications.Select(x => x.ProjectInfo))
                .FirstOrDefault(app => app.Id == user.DataId);
            
            // Pass into view
            return View(applicant);
        }

        // GET: Applications
        public ViewResult New(int id)
        {
            // Get Applications From Database
            NewApplicationViewModel viewModel = new NewApplicationViewModel(id);
            // Pass into view
            return View(viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create([ModelBinder(typeof(ProjectInfoModelBinder))] NewApplicationViewModel newApp)
        {
            var application = newApp.CreateApplication();
            var user = GetCurrentApplicant();
            user.Applications.Add(application);
            _context.SaveChanges();
            application.ProjectInfoId = application.ProjectInfo.Id;
            application.ProjectInfo.ApplicationId = application.Id;
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new { id = application.Id});
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([ModelBinder(typeof(FormModelBinder))] IForm editApp, int applicationId, IEnumerable<HttpPostedFileBase> files)
        {
            var application = this.GetCurrentApplicant().Applications.FirstOrDefault(x => x.Id == applicationId);

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var projectInfo = _context.Applications.Include(x=>x.ProjectInfo)
                .First(x=>x.Id == applicationId).ProjectInfo;

            var forms = application.Forms;

            // find edit app ID from forms list
            int editAppId = 0;
            for (int i = 0; i < forms.Length; i++)
            {
                if (forms[i] != null && forms[i].PropertyName == editApp.PropertyName)
                {
                    editAppId = i;
                    break;
                }
            }

            if (editApp is BasicProjectInfo)
            {
                projectInfo.CopyValues((BasicProjectInfo)editApp);
            }
            else
            {
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                        }
                    }
                }
                application.UpdateForm(editApp);
            }

            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new { id = application.Id, editId = editAppId});
        }

        public ActionResult Details(int id, int? editId)
        {
            AApplication application = _context.Applications
                .Include(a => a.ProjectInfo)
                .FirstOrDefault( a => a.Id == id);

            EditApplicationViewModel toEdit = new EditApplicationViewModel()
            {
                Application =  application,
                EditId = editId.HasValue ? editId.Value : 0
            };

            if (application == null)
                return HttpNotFound();
            
            return View(toEdit);
        }


        public Applicant GetCurrentApplicant()
        {
            var user = GetCurrentUser();
            return _context.Applicants.FirstOrDefault(app => app.Id == user.DataId);
        }

        public ApplicationUser GetCurrentUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}