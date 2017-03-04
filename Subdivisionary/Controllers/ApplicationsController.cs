using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Web.UI;
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
            var application = _context.Applications.Where(x => x.Id == id)
                .Include(x => x.Forms).FirstOrDefault();

            if (application == null || applicant.Applications.All(x => x.Id != application.Id))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            EditApplicationViewModel toEdit = new EditApplicationViewModel()
            {
                EditId = editId.HasValue ? editId.Value : 0,
                Application = application,
                Forms = application.GetOrderedForms()
            };

            return View(toEdit);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IForm editApp, int applicationId, int editId)
        {
            var applicant = GetCurrentApplicant();
            var application = _context.Applications.Where(x => x.Id == applicationId)
                .Include(x => x.Forms).FirstOrDefault();

            if (application == null || applicant.Applications.All(x => x.Id != application.Id))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            // Get current form
            var allforms = application.GetOrderedForms();
            IForm form = allforms[editId];

            // Ensure all files have been updated
            if (form is UploadableFileForm)
            {
                var errorProperty = ((UploadableFileForm) form).FindEmptyFileProperty();
                if(errorProperty.HasValue)
                    ModelState.AddModelError(errorProperty.Value.UniqueKey, "Please upload a " + errorProperty.Value.StandardName + " file");
            }

            // Ensure Model Validation
            if (!ModelState.IsValid)
            {
                // Copy Values of form into the model binded application.
                // This copies in "reverse" flow, meaning that everything that is not mdoel binded by default should be copied
                editApp.CopyValues(form, true);
                EditApplicationViewModel toEdit = new EditApplicationViewModel()
                {
                    EditId = editId,
                    Application = application,
                    Forms = application.GetOrderedForms(),
                    Form = editApp // Specify a form so that the Application form at the designated EditId isn't edited
                };
                return View("Details", toEdit);
            }

            // Ensure that nobody messed with the form params by checking application type before copying
            if (editApp.GetType() != ObjectContext.GetObjectType(form.GetType()))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // Notify all Observing forms within the application that a single form has been updated
            application.FormUpdated(_context, form, editApp);

            form.CopyValues(editApp);

            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new {id = application.Id, editId = editId});
        }

        #endregion
        
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
            return _context.Applicants.Where(app => app.Id == user.DataId)
                .Include(appl => appl.Applications)
                .FirstOrDefault();
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
            return RedirectToAction("Share", "Applications", new {id = applicationId});
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

            List<MailMessage> messages = new List<MailMessage>();
            foreach (var toshare in emailList)
            {
                if (!application.SharedRequests.Contains(toshare))
                {
                    MailMessage message = new MailMessage("ahmed.elzeiny2@sfdpw.org", toshare.EmailAddress)
                    {
                        Subject =
                            $"[Application ID #{applicationId}] {applicant.User.Name} invites you to work on a {application.DisplayName} Project"
                    };
                    EmailInviteViewModel invite = new EmailInviteViewModel()
                    {
                        Address = application.ProjectInfo.ToString(),
                        ToName = toshare.EmailAddress,
                        ApplicationDisplayName = application.DisplayName,
                    };
                    if (Request.Url != null)
                    {
                        invite.RegisterUrl = this.Url.Action("Register", "Account", new object(),  Request.Url.Scheme);
                        invite.ApplicationUrl = Url.Action("Share", "Applications", new {id = application.Id}, Request.Url.Scheme);
                    }
                    message.Body = RenderRazorViewToString("InviteEmail", invite);

                    message.IsBodyHtml = true;
                    // Send invite via email
                    messages.Add(message);
                }
            }
            await Contact(messages);
            application.SharedRequests.Clear();
            application.SharedRequests.AddRange(emailList);
            _context.SaveChanges();
            return RedirectToAction("Share", "Applications", new {id = applicationId});
        }
        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Contact(List<MailMessage> messages)
        {
            if (messages.Count != 0)
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential(ConfigurationManager.AppSettings["EmailAccountName"], ConfigurationManager.AppSettings["EmailAccountPass"]);
                    smtp.Credentials = credential;
                    smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
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
            UploadableFileForm form = mForm as UploadableFileForm;
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

                    var fileProps = form.GetFileUploadInfo(requestFiles.AllKeys[i]);

                    if (form.MaximumFileUploadsExceeded(fileProps))
                        throw new Exception("Maximum File Limit Exceeded");

                    var upload = new FileUploadInfo
                    {
                        FileKey = fileProps.UniqueKey,
                        Form = form,
                        FormId = form.Id
                    };
                    form.FileUploads.Add(upload);
                    await _context.SaveChangesAsync();

                    // Use FileProps & Upload ID to create unique name for naming convention
                    string fileName =
                        $"{fileProps.FolderPath}\\{appId}_{fileProps.StandardName}_{now:yyyyMMdd}_{upload.Id}{Path.GetExtension(file.FileName)}";
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

                    // Upload File to Azure Blob
                    cloudBlockBlob.Properties.ContentType = file.ContentType;
                    await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);

                    // Set the remaining upload properties with generated information. Save one last time.
                    upload.Size = cloudBlockBlob.Properties.Length;
                    upload.Type = cloudBlockBlob.Properties.ContentType;
                    upload.Url = cloudBlockBlob.Uri.AbsoluteUri;
                    await _context.SaveChangesAsync();

                    vm.AddFile(Url, upload.Id, form.Id, cloudBlockBlob.Uri.AbsoluteUri, upload.Type, upload.Size,
                        fileProps.StandardName);
                }
                var jsn = JsonConvert.SerializeObject(vm);
                return Content(jsn, "application/json");
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

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> DeleteFile(int id, int formId)
        {
            Form mForm = await _context.Forms.FindAsync(formId);
            UploadableFileForm form = mForm as UploadableFileForm;
            if(form == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var file = form.FileUploads.FirstOrDefault(x => x.Id == id);
            if (file == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.FileUploads.Remove(file);
            if (form.IsAssigned && form.FindEmptyFileProperty().HasValue)
                form.IsAssigned = false;
            await _context.SaveChangesAsync();
            string json = JsonConvert.SerializeObject("");
            return Content(json, "application/json");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Sign(int id, SignatureViewModel signature)
        {
            var mform = _context.Forms.Find(id);
            var applicant = GetCurrentApplicant();
            if (applicant.Applications.FirstOrDefault(x=>x.Id == mform.ApplicationId) == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var form = mform as SignatureForm;
            if(form == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest );
            var sig = new SignatureUploadInfo()
            {
                Data = signature.SignatureData,
                DataFormat = signature.SerializationType,
                DateStamp = DateTime.Now,
                SignerName = signature.SignerName,
                UserStamp = applicant.User.Name,
                SignatureForm = form,
                SignatureFormId = id
            };
            form.Signatures.Add(sig);
            _context.SaveChanges();
            signature.DateStamp = sig.DateStamp;
            signature.UserStamp = sig.UserStamp;
            signature.SignatureData = sig.Data;
            signature.SerializationType = sig.DataFormat;
            return Content(CustomHtmlHelper.EncodeJson(signature), "application/json");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Unsign(int id, SignatureViewModel signature)
        {
            var mform = _context.Forms.Find(id);
            var applicant = GetCurrentApplicant();
            if (applicant.Applications.FirstOrDefault(x => x.Id == mform.ApplicationId) == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var form = mform as ISignatureForm;
            if (form == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var toremove = form.Signatures.First(x => x.SignerName == signature.SignerName);
            _context.SignatureInfo.Remove(toremove);
            mform.IsAssigned = false;
            _context.SaveChanges();
            return Content("success");
        }
    }
}
 