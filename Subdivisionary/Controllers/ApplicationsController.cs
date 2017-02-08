﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Subdivisionary.DAL;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.Models.Validation;
using Subdivisionary.ViewModels;

namespace Subdivisionary.Controllers
{

    public class ApplicationsController : Controller
    {
        private ApplicationDbContext _context;

        public ApplicationsController()
        {
            _context = new ApplicationDbContext();
            _context.Configuration.ValidateOnSaveEnabled = false;
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

        #region Create & New Applications

        public ViewResult New(int id)
        {
            // Create Application given ApplicationTypeViewModel id
            NewApplicationViewModel viewModel = new NewApplicationViewModel()
            {
                ApplicationType = (ApplicationTypeViewModel) id,
                ProjectInfo = CreateApplication((ApplicationTypeViewModel) id).ProjectInfo
            };
            // Pass into view
            return View(viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(IForm projectInfo, int appTypeId)
        {
            if (!ModelState.IsValid)
                return View("New", new NewApplicationViewModel(appTypeId, (BasicProjectInfo) projectInfo));

            Application application = this.CreateApplication((ApplicationTypeViewModel) appTypeId);
            application.ProjectInfo = (BasicProjectInfo) projectInfo;
            var user = GetCurrentApplicant();
            user.Applications.Add(application);
            _context.SaveChanges();

            application.ProjectInfoId = application.ProjectInfo.Id;
            application.ProjectInfo.ApplicationId = application.Id;
            application.ProjectInfo.IsAssigned = true;
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new {id = application.Id});
        }

        public ActionResult SpeedTest()
        {
            return View(new OwnerForm());
        }

        private Application CreateApplication(ApplicationTypeViewModel appType)
        {
            Application answer = null;
            if (appType == ApplicationTypeViewModel.RecordOfSurvey)
                answer = Application.FactoryCreate<RecordOfSurvey>();

            else if (appType == ApplicationTypeViewModel.CcBypass)
                answer = Application.FactoryCreate<CcBypass>();
            else if (appType == ApplicationTypeViewModel.CcEcp)
                answer = Application.FactoryCreate<CcEcp>();
            else if (appType == ApplicationTypeViewModel.NewConstruction)
                answer = Application.FactoryCreate<NewConstruction>();

            else if (appType == ApplicationTypeViewModel.LotLineAdjustment)
                answer = Application.FactoryCreate<LotLineAdjustment>();
            else if (appType == ApplicationTypeViewModel.CertificateOfCompliance)
                answer = Application.FactoryCreate<CertificateOfCompliance>();
            else if (appType == ApplicationTypeViewModel.LotMerger)
                answer = Application.FactoryCreate<LotMerger>();
            else if (appType == ApplicationTypeViewModel.LotSubdivision)
                answer = Application.FactoryCreate<LotSubdivision>();
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return answer;
        }

        #endregion

        #region Details & Edit Applications

        public ActionResult Details(int id, int? editId)
        {
            var applicant = GetCurrentApplicant();
            var application = (applicant.Applications.FirstOrDefault(x => x.Id == id));

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            EditApplicationViewModel toEdit = new EditApplicationViewModel()
            {
                EditId = editId.HasValue ? editId.Value : 0,
                Application = application
            };

            return View(toEdit);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(IForm editApp, int applicationId, int editId)
        {
            // ensure that application belongs to user
            Application application = this.GetCurrentApplicant().Applications.FirstOrDefault(x => x.Id == applicationId);
            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            // Get current form
            var allforms = application.GetOrderedForms();
            IForm form = allforms[editId];

            // Ensure Model Validation
            if (!ModelStateIsValid(ModelState))
            {
                EditApplicationViewModel toEdit = new EditApplicationViewModel()
                {
                    EditId = editId,
                    Application = application
                };
                return View("Details", toEdit);
            }

            // Ensure that nobody messed with the form params by checking application type before copying
            if (editApp.GetType() != ObjectContext.GetObjectType(form.GetType()))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // Notify all Observing forms within the application that a single form has been updated
            application.FormUpdated(form, editApp);

            form.CopyValues(editApp);

            // Sync File Uploads from server
            /*IUploadableFileForm fileForm = form as IUploadableFileForm;
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
                    string directory = Path.Combine(Server.GetApplicationDirectory(application),
                        uploadProperty.FolderPath);
                    DirectoryHelper.EnsureDirectoryExists(directory);
                    FileUploadList savedBasicFiles = fileForm.GetFileUploadList(uploadProperty.UniqueKey);
                    if (uploadProperty.IsSingleUpload && savedBasicFiles.Count > 0)
                        new FileInfo(Server.MapPath(savedBasicFiles.First().Url)).Delete();
                    string fileName = DirectoryHelper.FindUntakenFilename(directory, uploadProperty.StandardName,
                        Path.GetExtension(file.FileName));
                    file.SaveAs(fileName);
                    fileForm.SyncFile(uploadProperty.UniqueKey, Server.UnmapPath(fileName));
                }
            }*/
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new {id = application.Id, editId = editId});
        }

        /// <summary>
        /// Checks if model state is valid, taking into consideration BackgroundValidations.
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        private bool ModelStateIsValid(ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.IsValid;
        }

        #endregion

        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Debug()
        {
            return View();
        }

        public ActionResult Review(int id)
        {
            var applicant = GetCurrentApplicant();
            var application = (applicant.Applications.FirstOrDefault(x => x.Id == id));
            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return View(application);
        }

        public FileResult Download(string file)
        {
            if (!Server.FilePathExists(file))
                throw new HttpResponseException(HttpStatusCode.Gone);
            string appId = DirectoryHelper.GetApplicationIdFromFilePath(file);
            // for security purposes we have to ensure that the download link is coming from the right applicant
            if (GetCurrentApplicant().Applications.All(x => x.Id.ToString() != appId))
                throw new HttpResponseException(HttpStatusCode.BadGateway);
            // Read all fileBytes to memory & initiate download
            return File(System.IO.File.ReadAllBytes(Server.MapPath(file)),
                System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(file));
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


        /// <summary>
        /// Add email addresses to an application
        /// </summary>
        /// <param name="id">application id</param>
        public ActionResult Share(int id)
        {
            var applicant = GetCurrentApplicant();
            var application = _context.Applications.Find(id);

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            ShareApplicationViewModel vm = new ShareApplicationViewModel(application, applicant);

            // If applicant is not registered with application
            if (application.Applicants.All(x => x.Id != applicant.Id))
            {
                // If on share request list
                if (application.SharedRequests.Any(x => x.EmailAddress == applicant.User.Email))
                    return View("AcceptShare", vm);
            }
            else
                return View(vm);

            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        public ActionResult RemoveApplicationApplicants(
            [ModelBinder(typeof(ApplicantionApplicantModelBinder))] List<string> toRemove, int applicationId)
        {
            var applicant = GetCurrentApplicant();
            var application = (applicant.Applications.FirstOrDefault(x => x.Id == applicationId));

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            foreach (var removable in toRemove)
            {
                var removableApplicant = application.Applicants.First(x => x.User.Email == removable);
                removableApplicant.Applications.Remove(application);
                application.Applicants.Remove(removableApplicant);
            }
            _context.SaveChanges();
            return RedirectToAction("Review", "Applications", new {id = applicationId});
        }

        public ActionResult AcceptInvitation(int id)
        {
            var applicant = GetCurrentApplicant();
            var application = _context.Applications.Find(id);

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            EmailInfo info = new EmailInfo(applicant.User.Email);
            int index = application.SharedRequests.IndexOf(info);
            if (index < 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            application.SharedRequests.RemoveAt(index);
            application.Applicants.Add(applicant);
            applicant.Applications.Add(application);
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new {id = id});
        }

        public async Task<ActionResult> UpdateApplicationInvites(
            [ModelBinder(typeof(ApplicationInvitesModelBinder))] List<EmailInfo> emailList, int applicationId)
        {
            var applicant = GetCurrentApplicant();
            var application = (applicant.Applications.FirstOrDefault(x => x.Id == applicationId));

            if (application == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (!ModelState.IsValid)
            {
                ShareApplicationViewModel vm = new ShareApplicationViewModel(application, applicant);
                vm.ShareRequests.Clear();
                vm.ShareRequests.AddRange(emailList);
                return View("Share", vm);
            }

            foreach (var toshare in emailList)
            {
                List<MailMessage> messages = new List<MailMessage>();
                if (!application.SharedRequests.Contains(toshare))
                {
                    MailMessage message = new MailMessage("ahmed.elzeiny2@sfdpw.org", toshare.EmailAddress);
                    message.Subject =
                        $"[Application ID #{applicationId}] {applicant.User.Email} invites you to work a {application.DisplayName}";
                    message.Body =
                        $"<p>Dear {toshare.EmailAddress},</p><p>You are invited to edit the following project </p><strong>{application.ProjectInfo}</strong>" +
                        $"<p>Blah Blah Blah. Test. Test. Test. Can Office 365 server send emails on my behalf? That would be cool. If this works, please disregard. </p>";
                    message.IsBodyHtml = true;
                    // Send invite via email
                    messages.Add(message);
                }
                await Contact(messages);
            }
            application.SharedRequests.Clear();
            application.SharedRequests.AddRange(emailList);
            _context.SaveChanges();
            return RedirectToAction("Share", "Applications", new {id = applicationId});
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(List<MailMessage> messages)
        {
            if (messages.Count != 0)
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential("ahmed.elzeiny2@sfdpw.org", "$Allah1999");
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    foreach (var msg in messages)
                        await smtp.SendMailAsync(msg);
                }
            }
            return Content("SENT");
        }

        /// <summary>
        /// Upload File from designated Form
        /// </summary>
        /// <param name="id">Form Id</param>
        /// <returns></returns>
        public async Task<ActionResult> UploadFiles(int id)
        {
            Form mForm = await _context.Forms.FindAsync(id);
            IUploadableFileForm form = mForm as IUploadableFileForm;
            try
            {
                if (form == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                int appId = mForm.ApplicationId;
                // ensure that application belongs to user
                Application application = this.GetCurrentApplicant().Applications.FirstOrDefault(x => x.Id == appId);
                if (application == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                // Access Blob Storage
                string containerName = CloudHelper.GetContainerName(application);
                CloudStorageAccount cloudStorageAccount = CloudHelper.GetConnectionString();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await
                        cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        });
                }
                var requestFiles = HttpContext.Request.Files;

                FileUploadJsonViewModel vm = FileUploadJsonViewModel.Create();
                for (int i = 0; i < requestFiles.Count; i++)
                {
                    var file = requestFiles[i];
                    if (file == null || file.ContentLength <= 0)
                        continue;
                    var now = DateTime.Now;
                    var fileProps = form.FileUploadProperties().First(x => x.UniqueKey == requestFiles.AllKeys[i]);
                    string fileName =
                        $"{fileProps.FolderPath}\\{appId}_{fileProps.StandardName}_{now:yyyyMMdd}_{now.Ticks}{Path.GetExtension(file.FileName)}";
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    cloudBlockBlob.Properties.ContentType = file.ContentType;
                    //await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);
                    form.SyncFile(requestFiles.AllKeys[i], new FileUploadInfo()
                    {
                        Size = cloudBlockBlob.Properties.Length,
                        Type = cloudBlockBlob.Properties.ContentType,
                        Url = cloudBlockBlob.Uri.AbsolutePath
                    });
                    await _context.SaveChangesAsync();
                    vm.AddFile(Url, cloudBlockBlob.Uri.AbsoluteUri, cloudBlockBlob.Properties, fileProps);
                }
                return Content(JsonConvert.SerializeObject(vm), "application/json");
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(new
                {
                    error = ex.Message
                });
                return Content(json, "application/json");
            }
        }

        public async Task<ActionResult> DeleteFile(int formId, string uniqueKey, string fileUrl)
        {
            throw new NotImplementedException("Not yet");
        }
    }
}