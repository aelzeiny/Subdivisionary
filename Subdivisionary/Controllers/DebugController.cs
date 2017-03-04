using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
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
using Subdivisionary.Models.ProjectInfos;
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

        public ActionResult Email()
        {
            return View("InviteEmail", new EmailInviteViewModel() {Address = "123 Hello World St", ApplicationDisplayName = "Record of Survey", ApplicationUrl = "www.google.com", RegisterUrl = "www.google.com", ToName = "Ahmed"});
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
            var application = Application.FactoryCreate<LotLineAdjustment>();
            var pinfo = (CocAndLlaProjectInfo)application.ProjectInfo;
            pinfo.PrimaryContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test@test.com",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            pinfo.OwnerContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            pinfo.LandFirmContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            pinfo.DeveloperContactInfo = new ContactInfo()
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

        public ActionResult CreateInvoice()
        {
            var xml = new InvoiceTypeXml(PaymentGatewaySoapHelper.CallWebService(new ListInvoiceEnvelope()));
            var invoiceType = xml.FindInvoiceByType(InvoiceTypeXml.INVOICE_TYPE_GENERAL);

            var fee = PaymentAccounts.MappingItem;
            string accnt = ((int)fee).ToString();
            var descr = ((DisplayAttribute)(typeof(PaymentAccounts).GetMember(fee.ToString())[0].GetCustomAttributes(typeof(DisplayAttribute), false)[0])).Name;
            var envelope = new CreateInvoiceEnvelope("Awkii", 0.01, "short description", "longer memo", accnt, descr, invoiceType.Id);
            var retval = PaymentGatewaySoapHelper.CallWebService(envelope);
            return Content(retval, "text/xml");
        }

    }
}