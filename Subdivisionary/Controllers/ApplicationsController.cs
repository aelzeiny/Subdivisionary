using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Subdivisionary.DAL;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
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
            _context = new ApplicationDbContext();// {ProxyEnabled = false};
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
            NewApplicationViewModel viewModel = new NewApplicationViewModel<BasicProjectInfo>(id);
            // Pass into view
            return View(viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create([ModelBinder(typeof(DebugModelBinder))] NewApplicationViewModel newApp)
        {
            //ReverifyModel();
            var application = newApp.CreateApplication();
            var user = GetCurrentApplicant();
            user.Applications.Add(application);
            if (!ModelState.IsValid)
                return View("New", newApp);
            _context.SaveChanges();
            application.ProjectInfoId = application.ProjectInfo.Id;
            application.ProjectInfo.ApplicationId = application.Id;
            application.ProjectInfo.IsAssigned = true;
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new { id = application.Id});
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([ModelBinder(typeof(FormModelBinder))] IForm editApp, int applicationId, int editId)
        {
            // ensure that application belongs to user
            Application application = this.GetCurrentApplicant().Applications.FirstOrDefault(x => x.Id == applicationId);
            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            // Get current form
            var allforms = application.GetOrderedForms();
            IForm form = allforms[editId];

            // Ensure Model Validation
            if (!ModelState.IsValid)
            {
                EditApplicationViewModel toEdit = new EditApplicationViewModel()
                {
                    Forms = application.GetOrderedForms(),
                    EditId = editId,
                    DisplayName = application.DisplayName,
                    ApplicationId = applicationId
                };
                return View("Details", toEdit);
            }

            // Ensure that nobody messed with the form params by checking application type before copying
            if (editApp.GetType() != ObjectContext.GetObjectType(form.GetType()))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            form.CopyValues(editApp);

            // Sync File Uploads from server
            IUploadableFileForm fileForm = form as IUploadableFileForm;
            if (fileForm != null)
            {
                var requestFiles = HttpContext.Request.Files;
                FileUploadProperty[] fileUploadProperty = fileForm.FileUploadProperties();
                for (int i = 0; i < requestFiles.Count; i++)
                {
                    var file = requestFiles[i];
                    if (file == null || file.ContentLength <= 0)
                        continue;
                    string key = requestFiles.AllKeys[i];
                    FileUploadProperty uploadProperty = fileUploadProperty.FirstOrDefault(x => x.UniqueKey == key);
                    if (uploadProperty.UniqueKey != key) // if no key found
                        continue; // continue to next file
                    
                    // Now upload all files
                    string directory = Path.Combine(Server.GetApplicationDirectory(application), uploadProperty.FolderPath);
                    DirectoryHelper.EnsureDirectoryExists(directory);
                    FileUploadList savedBasicFiles = fileForm.GetFileUploadList(uploadProperty.UniqueKey);
                    if (uploadProperty.IsSingleUpload && savedBasicFiles.Count > 0)
                        new FileInfo(Server.MapPath(savedBasicFiles.First().FilePath)).Delete();
                    string fileName = DirectoryHelper.FindUntakenFilename(directory, uploadProperty.StandardName, Path.GetExtension(file.FileName));
                    file.SaveAs(fileName);
                    fileForm.SyncFile(uploadProperty.UniqueKey, Server.UnmapPath(fileName));
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new { id = application.Id, editId = editId});
        }
        
        public ActionResult Details(int id, int? editId)
        {
            var applicant = GetCurrentApplicant();
            var application = (applicant.Applications.FirstOrDefault(x => x.Id == id));

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            EditApplicationViewModel toEdit = new EditApplicationViewModel()
            {
                Forms =  application.GetOrderedForms(),
                EditId = editId.HasValue ? editId.Value : 0,
                DisplayName =  application.DisplayName,
                ApplicationId = application.Id
            };
            
            return View(toEdit);
        }

        public FileResult Download(string file)
        {
            if(!Server.FilePathExists(file))
                throw new HttpResponseException(HttpStatusCode.Gone);
            string appId = DirectoryHelper.GetApplicationIdFromFilePath(file);
            // for security purposes we have to ensure that the download link is coming from the right applicant
            if (GetCurrentApplicant().Applications.All(x => x.Id.ToString() != appId))
                throw new HttpResponseException(HttpStatusCode.BadGateway);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(file));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, System.IO.Path.GetFileName(file));
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