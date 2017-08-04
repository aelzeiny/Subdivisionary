using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using reCaptcha;
using Subdivisionary.DAL;
using Subdivisionary.ExternalApis;
using Subdivisionary.Helpers;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels.ApplicationViewModels;

namespace Subdivisionary.Controllers
{
    using HttpResponseException = System.Web.Http.HttpResponseException;
    public class ApplicationsController : AContextController
    {
        public ApplicationsController() :base()
        {
            _context.Configuration.ValidateOnSaveEnabled = false;
        }

        // GET: Applications
        public ViewResult Index()
        {
            if (User.IsInRole(EUserRoles.Admin.ToString()) || User.IsInRole(EUserRoles.Bsm.ToString()))
            {
                var list = _context.Applications.Include(x => x.StatusHistory).Include(x => x.ProjectInfo).ToList();
                var mlist = list.Where(x => x.HasStatus(EApplicationStatus.Submitted)
                                    && !x.HasStatus(EApplicationStatus.Done))
                                    .Select(x=>new ApplicationIndexViewModel()
                        {
                            ApplicationId = x.Id,
                            DisplayName = x.DisplayName,
                            ApplicationStatus = x.CurrentStatusLog.Status,
                            BlockLots = x.ProjectInfo.Apns(),
                            Addresses = x.ProjectInfo.Addresses()
                        });
                return View("IndexBsm", new ApplicationIndexSearchViewModel() {SearchQuery = new SearchViewModel(), Results = mlist});

            }
            var user = GetCurrentUser();
            // Get Applications From Database
            var applicant = _context.Applicants
                .Include(app => app.Applications.Select(x => x.ProjectInfo))
                .FirstOrDefault(app => app.Id == user.DataId);
            return View(applicant);
            // Pass into view
        }

        #region Create & New Applications
        public ViewResult New(int id)
        {
            // Create Application given EApplicationTypeViewModel id
            NewApplicationViewModel viewModel = new NewApplicationViewModel()
            {
                ApplicationType = (EApplicationTypeViewModel) id,
                ProjectInfo = CreateApplication((EApplicationTypeViewModel) id).ProjectInfo
            };
            // Pass into view
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(IForm projectInfo, int appTypeId)
        {
            if (!ModelState.IsValid)
                return View("New", new NewApplicationViewModel(appTypeId, (BasicProjectInfo) projectInfo));

            Application application = this.CreateApplication((EApplicationTypeViewModel) appTypeId);
            application.ProjectInfo = (BasicProjectInfo) projectInfo;
            application.FormUpdated(_context, projectInfo, projectInfo);
            var user = GetCurrentApplicant();
            user.Applications.Add(application);
            _context.SaveChanges();

            application.ProjectInfoId = application.ProjectInfo.Id;
            application.ProjectInfo.ApplicationId = application.Id;
            application.ProjectInfo.IsAssigned = true;
            _context.SaveChanges();
            // Notify External Apis of New Application creation. Most Apis 
            // would ignore this event until application submitted with a paid fee,
            // but the option is there none-the-less
            ExternalApiManager.StatusChangedTriggerInBackground(application);

            return RedirectToAction("Details", "Applications", new {id = application.Id});
        }

        private Application CreateApplication(EApplicationTypeViewModel appType)
        {
            Application answer = null;
            if (appType == EApplicationTypeViewModel.RecordOfSurvey)
                answer = Application.FactoryCreate<RecordOfSurvey>();
            // Condos
            else if (appType == EApplicationTypeViewModel.CcBypass)
                answer = Application.FactoryCreate<CcBypass>();
            else if (appType == EApplicationTypeViewModel.CcEcp)
                answer = Application.FactoryCreate<CcEcp>();
            else if (appType == EApplicationTypeViewModel.NewConstruction)
                answer = Application.FactoryCreate<NewConstruction>();
            // CoC, LLA, LM, VS, & LS
            else if (appType == EApplicationTypeViewModel.LotLineAdjustment)
                answer = Application.FactoryCreate<LotLineAdjustment>();
            else if (appType == EApplicationTypeViewModel.CertificateOfCompliance)
                answer = Application.FactoryCreate<CertificateOfCompliance>();
            else if (appType == EApplicationTypeViewModel.ParcelFinalMap)
                answer = Application.FactoryCreate<ParcelFinalMap>();
            // Sidewalk
            else if(appType == EApplicationTypeViewModel.SidewalkLegislation)
                answer = Application.FactoryCreate<SidewalkLegislation>();
            // Other
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return answer;
        }

        #endregion

        #region Details & Edit Applications
        /// <summary>
        /// Generates Editing page for a given application ID and form #
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <param name="formId">if 0 or unassigned then default to Project Info</param>
        /// <returns></returns>
        public ActionResult Details(int id, int? formId)
        {
            if(User.IsInRole(EUserRoles.Admin.ToString()) || User.IsInRole(EUserRoles.Bsm.ToString()))
                return RedirectToAction("Submitted", new { id = id });
            var applicant = GetCurrentApplicant();
            var application = _context.Applications.Where(x => x.Id == id)
                .Include(x => x.Forms).FirstOrDefault();
            if (application == null || applicant.Applications.All(x => x.Id != application.Id))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (!application.CanEdit)
                return RedirectToAction("Submitted", new {id = id});

            EditApplicationViewModel toEdit = new EditApplicationViewModel()
            {
                FormId = formId.HasValue ? formId.Value : 0,
                Application = application,
                Forms = application.GetOrderedForms()
            };

            return View(toEdit);
        }

        /// <summary>
        /// Validates and saves an edited application form.
        /// </summary>
        /// <param name="editApp">IForm that is model-binded with the submitted form. Uses the 'CustomModelBinder' class to save form info to IForm.</param>
        /// <param name="id">Id of the given Application</param>
        /// <param name="formId">Id of the given form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, int formId, IForm editApp)
        {
            IForm form = (editApp is Form)
                ? _context.Forms.Where(x => x.Id == formId).Include(x => x.Application).FirstOrDefault()
                : (IForm) _context.ProjectInfos.Where(x => x.Id == formId).Include(x => x.Application).FirstOrDefault();
            var applicant = this.GetCurrentApplicant();
            if (form == null || applicant.Applications.All(x => x.Id != form.ApplicationId))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var application = form.Application;
            if (!application.CanEdit && !(form is PaymentForm))
                return RedirectToAction("Review", new { id = form.ApplicationId });
            // Get current form

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
                    Application = application,
                    Forms = application.GetOrderedForms(),
                    FormId = form.Id,
                    Form = editApp  // Optional Value, lets the UI display the form that 
                                    //was marked incorrect (instead of displaying an empty form)
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
            return RedirectToAction("Details", "Applications", new {id = application.Id, formId = form is BasicProjectInfo ? 0 : form.Id });
        }
        #endregion


        #region Delete Application & Add/Delete Forms
        [HttpPost]
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = _context.Applications.Include(x => x.Forms).FirstOrDefault(x=>x.Id == id);
            if (toDelete == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var toDeleteList = toDelete.Forms.ToList();
            foreach (var me in toDeleteList)
                await DeleteForm(me.Id, me.ApplicationId);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult SwapForms(int id)
        {
            var toEdit = _context.Applications.Include(x => x.Forms).FirstOrDefault(x => x.Id == id);
            if (toEdit == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var formT = typeof(Form);
            var options = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && formT.IsAssignableFrom(t) && !t.IsAbstract && t != typeof(PaymentForm) &&
                t.Namespace == "Subdivisionary.Models.Forms");
            var swap = new SwapFormsViewModel() {ApplicationId = toEdit.Id, Forms = toEdit.Forms.ToList(), Options = options};
            return View("SwapForms", swap);
        }

        [HttpPost]
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult AddForm(AddFormViewModel vm)
        {
            var toEdit = _context.Applications.Include(x => x.Forms).FirstOrDefault(x => x.Id == vm.ApplicationId);
            if (toEdit == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Form form = (Form) Activator.CreateInstance(Type.GetType(vm.FormTypeName));
            toEdit.Forms.Add(form);
            _context.SaveChanges();
            return RedirectToAction("SwapForms", new {id = vm.ApplicationId});
        }

        [HttpPost]
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public async Task<ActionResult> DeleteForm(int id, int formId)
        {
            var form = _context.Forms.Find(formId);
            var uform = form as UploadableFileForm;
            if (uform != null)
            {
                var controller = DependencyResolver.Current.GetService<FileController>();
                controller.ControllerContext = new ControllerContext(Request.RequestContext, controller);
                using (controller)
                {
                    var uploads = uform.FileUploads.ToList().Select(x => x.Id);
                    foreach (var uploadId in uploads)
                        await controller.DeleteFile(uploadId, uform.Id);
                }
            }
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Review(int id)
        {
            var application = _context.Applications.Where(x => x.Id == id)
                .Include(x => x.StatusHistory)
                .Include(x => x.Forms).FirstOrDefault();
            if (User.IsInRole(EUserRoles.Admin.ToString()) || User.IsInRole(EUserRoles.Bsm.ToString()))
                return View("ReviewBsm", application);

            var user = GetCurrentApplicant();
            var applicant = user.Applications.FirstOrDefault(x => x.Id == application.Id);
            if (applicant == null)
                return RedirectToAction("Index");
            var review = application.Review();
            foreach (var result in review)
                ModelState.AddModelError("", result);

            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            return View("Review", application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(int id)
        {
            if (!ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha:SecretKey"]) || !ModelState.IsValid)
            {
                ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(this.HttpContext);
                return RedirectToAction("Review", new {id = id});
            }
            var applicant = GetCurrentApplicant();
            var application =
                _context.Applications.Where(x => x.Id == id)
                .Include(x => x.StatusHistory)
                .Include(x=>x.Forms)
                .FirstOrDefault();

            if(application == null)
                return RedirectToAction("Index");
            if (application.Applicants.All(x=>x.Id != applicant.Id))
                return RedirectToAction("Index");

            // Find Fee Schedule Item
            if (!application.SubmitAndFinalize())
                return RedirectToAction("Review", new { id = id });
            if (application.CurrentStatusLog.Status == EApplicationStatus.Submitted)
                GenerateInvoice(application, EInvoicePurpose.InitialPayment);
            _context.SaveChanges();

            // Notify External APIs of this event whether it is an initial or secondary submittal
            ExternalApiManager.StatusChangedTriggerInBackground(application);

            return RedirectToAction("Submitted", new {id = id});
        }

        private void GenerateInvoice(Application application, EInvoicePurpose invoicePurpose)
        {
            var feeName = application.PaymentSchedule.ToString();
            var feeScheduleItem = _context.FeeSchedule.First(x => x.ApplicationTypeName == feeName);
            int numberOfUnits = (application.ProjectInfo is IUnitCount)
                ? ((IUnitCount) application.ProjectInfo).UnitCount
                : 0;
            float fee = feeScheduleItem.CalculateProcessingFee(numberOfUnits);
            if (invoicePurpose == EInvoicePurpose.IncompleteFee)
                fee = EFeeSchedule.INCOMPLETE_FEE;
            else if(invoicePurpose == EInvoicePurpose.MapReviewFee)
                fee = feeScheduleItem.CalculateMapReviewFee(numberOfUnits);

            // We forgive fees less than a nickle I guess. CCSF can bill James Ryan the damages ^_^
            if (Math.Abs(fee) < .05f)
                return; // Don't generate an invoice. 

            var invoiceEnvelope = ApplicationToInvoice(fee, application);
            var invoice = new CreateInvoiceXml(PaymentGatewaySoapHelper.CallWebService(invoiceEnvelope));
            var invoiceRead =
                new ReadInvoiceXml(PaymentGatewaySoapHelper.CallWebService(new ReadInvoiceEnvelope(invoice.Id)));

            InvoiceInfo finalInvoice = new InvoiceInfo()
            {
                InvoiceNo = invoice.Id,
                PayUrl = invoice.Payurl,
                PrintUrl = invoice.Printurl,
                Created = invoiceRead.Created,
                Paid = invoiceRead.IsPaid,
                Void = invoiceRead.Voided,
                Amount = fee.ToString("c2"),
                InvoicePurpose = invoicePurpose
            };

            var paymentForm = new PaymentForm();
            application.Forms.Add(paymentForm);
            _context.SaveChanges();
            finalInvoice.PaymentForm = paymentForm;
            finalInvoice.PaymentFormId = paymentForm.Id;
            _context.Invoices.Add(finalInvoice);
            _context.SaveChanges();

            paymentForm.InvoiceId = finalInvoice.Id;
            paymentForm.Invoice = finalInvoice;

            // Notify External APIs of this event
            ExternalApiManager.InvoiceGeneratedTriggerInBackground(application, finalInvoice);
        }
        
        public ActionResult Submitted(int id)
        {
            var applicant = GetCurrentApplicant();
            var application =
                _context.Applications.Where(x => x.Id == id)
                .Include(x => x.StatusHistory)
                .Include(x => x.Forms)
                .FirstOrDefault();

            if (application == null)
                return RedirectToAction("Index");
            if (!User.IsInRole(EUserRoles.Bsm) && !User.IsInRole(EUserRoles.Admin))
            {
                if (application.Applicants.All(x => x.Id != applicant.Id))
                    return RedirectToAction("Index");
            }

            // Create Application Submitted View Model
            ApplicationSubmittedViewModel vm = new ApplicationSubmittedViewModel()
            {
                ApplicationId = application.Id,
                NextSteps = application.NextSteps(),
                Invoices = application.Forms.OfType<PaymentForm>().Select(x=>_context.Invoices.Find(x.InvoiceId)).ToList(),
                Statuses = application.StatusHistory.ToList(),
                ApplicationCanEdit = application.CanEdit
            };
            return View((!User.IsInRole(EUserRoles.Bsm) && !User.IsInRole(EUserRoles.Admin)) ? "Submitted" : "SubmittedBsm", vm);
        }

        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult ApplicationDeemedIncomplete(int id, bool chargeFee)
        {
            var application = _context.Applications.FirstOrDefault(x => x.Id == id);
            if (application == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            application.StatusHistory.Add(ApplicationStatusLogItem.FactoryCreate(EApplicationStatus.DeemedIncomplete));
            if (chargeFee)
                GenerateInvoice(application, EInvoicePurpose.IncompleteFee);
            application.CanEdit = true;
            _context.SaveChanges();
            // Notify External APIs of this event asynchronously
            ExternalApiManager.StatusChangedTriggerInBackground(application);
            return RedirectToAction("Submitted", new {id = id});
        }

        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult ApplicationDeemedSubmittable(int id)
        {
            var application = _context.Applications.FirstOrDefault(x => x.Id == id);
            if (application == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            application.StatusHistory.Add(ApplicationStatusLogItem.FactoryCreate(EApplicationStatus.DeemedSubmittable));
            GenerateInvoice(application, EInvoicePurpose.MapReviewFee);
            _context.SaveChanges();
            // Notify External APIs of this event asynchronously
            ExternalApiManager.StatusChangedTriggerInBackground(application);
            return RedirectToAction("Submitted", new { id = id });
        }

        /// <summary>
        /// Clients can pay for invoices with this Action.
        /// Either by Card or by check, the process is essentially the same
        /// </summary>
        /// <param name="id">Invoice No, NOT the Id</param>
        /// <returns>View & Viewmodel for Invoice Payment</returns>
        public ActionResult Invoices(int id)
        {
            var invoice = _context.Invoices.Where(x => x.InvoiceNo == id).Include(x=>x.PaymentForm).FirstOrDefault();
            if(invoice == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (User.IsInRole(EUserRoles.Admin) || User.IsInRole(EUserRoles.Bsm))
            {
                return View("InvoicesBsm", invoice);
            }
            var aid = invoice.PaymentForm.ApplicationId;
            var application = _context.Applications.Where(x => x.Id == aid).Include(x => x.Applicants).Include(x => x.ProjectInfo).FirstOrDefault();
            var applicant = GetCurrentApplicant();
            // Ensure that an applicant is looking at their invoice & not somebody else's
            if (application == null || application.Applicants.All(x => x.Id != applicant.Id))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            // Now we do the invoice payment
            return View("Invoices", invoice);
        }

        [HttpPost]
        public ActionResult CheckInvoice(int id)
        {
            var invoice = _context.Invoices.Where(x => x.Id == id).Include(x => x.PaymentForm).FirstOrDefault();

            if (invoice == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (!invoice.Paid)
            {
                var aid = invoice.PaymentForm.ApplicationId;
                var application = _context.Applications.Where(x => x.Id == aid)
                    .Include(x=>x.StatusHistory)
                    .Include(x => x.Applicants)
                    .Include(x => x.ProjectInfo)
                    .FirstOrDefault();
                var applicant = GetCurrentApplicant();
                if (application == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                if (!User.IsInRole(EUserRoles.Admin) && !User.IsInRole(EUserRoles.Bsm))
                {
                    // Ensure that an applicant is looking at their invoice & not somebody else's
                    if (application.Applicants.All(x => x.Id != applicant.Id))
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                // Verify Payment
                var invoiceRead =
                    new ReadInvoiceXml(PaymentGatewaySoapHelper.CallWebService(new ReadInvoiceEnvelope(invoice.InvoiceNo)));

                invoice.Paid = invoiceRead.IsPaid && !invoiceRead.Voided;
                invoice.Void = invoiceRead.Voided;

                if (invoice.Paid)
                {
                    EApplicationStatus status;
                    if(invoice.InvoicePurpose == EInvoicePurpose.InitialPayment)
                        status = EApplicationStatus.InitialPaymentReceived;
                    else if (invoice.InvoicePurpose == EInvoicePurpose.IncompleteFee)
                        status = EApplicationStatus.IncompleteFeeReceived;
                    else if(invoice.InvoicePurpose == EInvoicePurpose.MapReviewFee)
                        status = EApplicationStatus.MapReviewFeeReceived;
                    else
                        throw new NotSupportedException(EnumHelper<EInvoicePurpose>.GetDisplayValue(invoice.InvoicePurpose));
                    application.StatusHistory.Add(ApplicationStatusLogItem.FactoryCreate(status));

                    // Notify External APIs of this event
                    ExternalApiManager.StatusChangedTriggerInBackground(application);
                }
                _context.SaveChanges();
            }
            return Json(new { paid = invoice.Paid, voided = invoice.Void, success = invoice.Paid || invoice.Void });
        }

        [HttpPost]
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult VoidInvoice(VoidViewModel vm)
        {
            var invoice = _context.Invoices.Where(x => x.Id == vm.InvoiceId).Include(x => x.PaymentForm).FirstOrDefault();
            if (invoice == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            // Verify Payment
            var voidTry =
                new VoidInvoiceXml(PaymentGatewaySoapHelper.CallWebService(new VoidInvoiceEnvelope(invoice.InvoiceNo, vm.UserName, vm.Reason)));

            var invoiceRead =
                new ReadInvoiceXml(PaymentGatewaySoapHelper.CallWebService(new ReadInvoiceEnvelope(invoice.InvoiceNo)));
            invoice.Void = invoiceRead.Voided;
            _context.SaveChanges();

            return Json(new { result = voidTry.Result, success = invoice.Void });
        }

        private CreateInvoiceEnvelope ApplicationToInvoice(float amount, Application app)
        {
            var xml = new InvoiceTypeXml(PaymentGatewaySoapHelper.CallWebService(new ListInvoiceEnvelope()));
            var eInvoiceType = InvoiceTypeXml.INVOICE_TYPE_GENERAL;
            if (app is CcEcp)
                eInvoiceType = InvoiceTypeXml.INVOICE_TYPE_ECP;
            else if (app is CcBypass)
                eInvoiceType = InvoiceTypeXml.INVOICE_TYPE_CONVERSION;
            var invoiceType = xml.FindInvoiceByType(eInvoiceType);

            int numberOfUnits = (app.ProjectInfo is IUnitCount)
                ? ((IUnitCount)app.ProjectInfo).UnitCount
                : 0;
            var cie = 
                new CreateInvoiceEnvelope(string.Join(", ", app.OwnersAndTenants.Where(x=> !x.IsTenant).Select(x=>x.Name)),
                999, // <- Should be changed to 'amount' variable. This will cause a BUG. 
                $"{app.ProjectInfo.Apns()} ({app.PaymentSchedule.ToString()})",
                numberOfUnits == 0 ? app.DisplayName : (numberOfUnits + " - " + app.DisplayName),
                EPaymentAccounts.MappingItem.AccountNum,
                EPaymentAccounts.MappingItem.Description,
                invoiceType.Id);
            return cie;
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
            var application = applicant.Applications.FirstOrDefault(x => x.Id == applicationId);

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

            // Try to remove notification if it exists
            var foundShareNotification =
                applicant.Notifications.FirstOrDefault(x => x is ShareApplicationNotification && ((ShareApplicationNotification) x).ApplicationId == id);
            if (foundShareNotification != null)
                applicant.Notifications.Remove(foundShareNotification);
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
            List<EmailInfo> newShares = new List<EmailInfo>();
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
                    newShares.Add(toshare);
                }
            }
            await Contact(messages);
            application.SharedRequests.AddRange(newShares);
            foreach (var newShare in newShares)
            {
                var foundApplicant = _context.Users.Include(x=>x.Data).FirstOrDefault(x => x.Email == newShare.EmailAddress);
                // Only Add notification if the applicant already exists.
                foundApplicant?.Data.Notifications.Add(new ShareApplicationNotification()
                {
                    Applicant = foundApplicant.Data,
                    ApplicantId = foundApplicant.DataId,
                    ApplicationId = application.Id
                });
            }
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

        [HttpPost]
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

        [HttpPost]
        [Authorize(Roles = EUserRoles.Admin + "," + EUserRoles.Bsm)]
        public ActionResult IndexSearch(ApplicationIndexSearchViewModel indexSearh)
        {
            var vm = indexSearh.SearchQuery;
            if (indexSearh.SearchQuery.ApplicationId.HasValue)
                return RedirectToAction("Details", new {id = indexSearh.SearchQuery.ApplicationId});
            IEnumerable<Application> query = _context.Applications
                .Include(x => x.Applicants)
                .Include(x => x.StatusHistory)
                .Include(x => x.ProjectInfo).AsEnumerable();
            if (!vm.BlockQuery.IsNullOrWhiteSpace())
                query = query.Where(x => x.ProjectInfo.AddressList.Any(y => y.Block.Contains(vm.BlockQuery)));
            if (!vm.LotQuery.IsNullOrWhiteSpace())
                query = query.Where(x => x.ProjectInfo.AddressList.Any(y => y.Block.Contains(vm.LotQuery)));
            if (!vm.AddressQuery.IsNullOrWhiteSpace())
                query = query.Where(x => x.ProjectInfo.AddressList.Any(y => y.Block.Contains(vm.AddressQuery)));
            if (!vm.UserQuery.IsNullOrWhiteSpace())
                query = query.Where(x => x.Applicants.Any(y=>y.User.UserName == vm.UserQuery));
            if (vm.Status != null)
                query = query.Where(x => x.CurrentStatusLog.Status == vm.Status);
            var result = new ApplicationIndexSearchViewModel()
            {
                Results = query.Select(x => new ApplicationIndexViewModel()
                {
                    Addresses = x.ProjectInfo.Addresses(),
                    ApplicationId = x.Id,
                    ApplicationStatus = x.CurrentStatusLog.Status,
                    BlockLots = x.ProjectInfo.Apns(),
                    DisplayName = x.DisplayName
                }),
                SearchQuery = vm
            };
            return View("IndexBsm", result);
        }

        [HttpPost]
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
            return Json(signature);
        }

        [HttpPost]
        public ActionResult Unsign(int id, SignatureViewModel signature)
        {
            var mform = _context.Forms.Find(id);
            var applicant = GetCurrentApplicant();
            var app = applicant.Applications.FirstOrDefault(x => x.Id == mform.ApplicationId);
            if (app == null || !app.CanEdit)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var form = mform as ISignatureForm;
            if (form == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var toremove = form.Signatures.First(x => x.SignerName == signature.SignerName);
            _context.SignatureInfo.Remove(toremove);
            mform.IsAssigned = false;
            _context.SaveChanges();
            return Json("success");
        }

        /// <summary>
        /// SUPER TEMPORARY SEED APPLICATION METHOD. REMOVE BEFORE DEPLOYMENT.
        /// Helps seed an application for developers by visiting a url.
        /// </summary>
        /// <returns></returns>
        public ActionResult SeedApplication()
        {
            var application = Application.FactoryCreate<NewConstruction>();
            var pinfo = (NewConstructionInfo)application.ProjectInfo;
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
                Email = "test@test.com",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            pinfo.LandFirmContactInfo = new ContactInfo()
            {
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                Email = "test@test.com",
                Name = "John Doe",
                State = "CA",
                Zip = "00000"
            };
            GetCurrentApplicant().Applications.Add(application);
            _context.Applications.Add(application);
            _context.SaveChanges();
            return RedirectToAction("Details", "Applications", new { id = application.Id });
        }
    }
}
 