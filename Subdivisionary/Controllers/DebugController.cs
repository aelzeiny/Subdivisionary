using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Subdivisionary.DAL;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;
using Subdivisionary.ViewModels;

namespace Subdivisionary.Controllers
{
    public class DebugController : Controller
    {
        private ApplicationDbContext _context;

        public DebugController()
        {
            _context = new ApplicationDbContext();
            _context.Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult UploadFileTest()
        {
            return View();
        }

        public ActionResult Signatures()
        {
            return View();
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

        public ActionResult RenderPdfAsHtml()
        {
            return View();
        }

        public ActionResult SeedApplication()
        {
            DebugApplication application = Application.FactoryCreate<DebugApplication>();
            application.ProjectInfo.PrimaryContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            application.ProjectInfo.OwnerContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            GetCurrentApplicant().Applications.Add(application);
            _context.Applications.Add(application);
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new {id = application.Id});
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Sign(SignatureViewModel signature)
        {
            System.Threading.Thread.Sleep(3000);
            return Content("hello world");
        }
    }
}
