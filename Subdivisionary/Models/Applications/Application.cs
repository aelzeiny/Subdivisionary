using Subdivisionary.Models.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.ProjectInfos;
using WebGrease.Css.Extensions;

namespace Subdivisionary.Models.Applications
{
    public abstract class Application
    {
        public static readonly int MAX_PARCEL_MAP_UNITS = 4;

        /**
         * This stores the id of this class
         */
        public int Id { get; set; }

        /// <summary>
        /// Nothing is being done with this field currently.
        /// </summary>
        public int Pid { get; set; }
        
        /**
         * All the forms (but not the project info form)
         */
        public virtual ICollection<Form> Forms { get; set; }

        /**
         * This stores the id of the corresponding Project Info field
         * This is using EntityFrameworks' Convention where BasicProjectInfo & ProjectInfoId are
         * automatically linked together.
         */
        public int ProjectInfoId { get; set; }

        public virtual BasicProjectInfo ProjectInfo { get; set; }
        
        /**
         * Each Application has one Applicant. This stores the applicant's ID & Applicant.
         * This is using EntityFrameworks' Convention where ApplicantID & Applicant are
         * automatically linked together.
         */
        [Required]
        public virtual ICollection<Applicant> Applicants { get; set; }

        /**
         * List of Status History denoting the current status, and the shift
         */
        public virtual ICollection<ApplicationStatusLogItem> StatusHistory { get; set; }

        /**
         * Determines whether applicant can edit the application, or the application is placed on hold
         */
        public bool CanEdit { get; set; }

        /// <summary>
        /// Current Status is the last item in status History
        /// </summary>
        public ApplicationStatusLogItem CurrentStatusLog => StatusHistory?.LastOrDefault();

        /// <summary>
        /// List of emails that are invited to edit the project.
        /// Okay, there's are reason why this is a SerializableList element, and not ID-based. It has to do with
        /// EF6 not being very kind to circular dependancies. Think of the relationship between notifications and applicants,
        /// applications and notifications, and applications to applicants.
        /// </summary>
        public EmailList SharedRequests { get; set; }
        public NamesList OwnersAndTenants { get; set; }

        /**
         * Display name of application
         */
        public abstract string DisplayName { get; }

        /**
         * Payment Schedule Type from Database
         */
        public abstract EFeeSchedule PaymentSchedule { get; }

        /**
         * Initial Fee of application
         */
        public string DirectoryName => this.Id + "_" + this.DisplayName;

        protected abstract Form[] GetDefaultApplicationForms();

        protected abstract void Init();

        protected Application()
        {
            SharedRequests = new EmailList();
            OwnersAndTenants = new NamesList();
            CanEdit = true;
        }

        /**
         * All the forms, in order, including the project info form
         */
        public virtual IList<IForm> GetOrderedForms()
        {
            List<IForm> answer = new List<IForm>();
            answer.Add(this.ProjectInfo);
            answer.AddRange(this.Forms);
            return answer;
        }

        /// <summary>
        /// Gets IForm using form type. If you define an application as a collection of forms,
        /// where no one form is repeated twice, this operator makes a lot of sense.
        /// </summary>
        /// <param name="key">Type of form needed</param>
        /// <returns>Returns null if no form found of given type</returns>
        protected virtual IForm this[Type key]
        {
            get
            {
                foreach (IForm form in this.GetOrderedForms())
                    if (form.GetType() == key)
                        return form;
                return null;
            }
        }

        /// <summary>
        /// This uses a modified version of the Observer Design pattern to notify
        /// all forms of the IObservable form interface when one form within the application 
        /// has changed. 
        /// </summary>
        /// <param name="modified"></param>
        public virtual void FormUpdated(ApplicationDbContext context, IForm before, IForm after)
        {
            // Maintain List of Tenants and Owners
            if (after is TenantForm)
            {
                var a = (TenantForm)after;
                // Ensure we have all the tenants added within the collection currently listed
                foreach (var t in a.TenantsList)
                {
                    if (OwnersAndTenants.All(x => x.Name != t.TenantName))
                        OwnersAndTenants.Add(new OccupantNameInfo() { IsTenant = true, Name = t.TenantName });
                }
                // Ensure we have no tenants in our collection that are no longer listed
                for (int i = OwnersAndTenants.Count - 1; i >= 0; i--)
                {
                    var t = OwnersAndTenants[i];
                    if (t.IsTenant && a.TenantsList.All(x => x.TenantName != t.Name))
                        OwnersAndTenants.RemoveAt(i);
                }
            }
            else if (after is OwnerForm)
            {
                var a = (OwnerForm)after;
                // Ensure we have all the tenants added within the collection currently listed
                foreach (var t in a.Owners)
                {
                    if (OwnersAndTenants.All(x => x.Name != t.OwnerName))
                        OwnersAndTenants.Add(new OccupantNameInfo() { IsTenant = false, Name = t.OwnerName });
                }
                // Ensure we have no tenants in our collection that are no longer listed
                for (int i = OwnersAndTenants.Count - 1; i >= 0; i--)
                {
                    var t = OwnersAndTenants[i];
                    if (t.IsTenant && a.Owners.All(x => x.OwnerName != t.Name))
                        OwnersAndTenants.RemoveAt(i);
                }
            }

            // Call all IObserverForms to update
            foreach (var form in Forms)
            {
                var observer = form as IObserverForm;
                if (observer != null)
                    observer.ObserveFormUpdate(context, before, after);
            }
        }

        /// <summary>
        /// Factory Design Pattern used to create a new Application. The constructor is reserved
        /// for Entity Framework by practice. Here we can create forms using their constructor and modify
        /// their variables (such as requirement).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FactoryCreate<T>() where T : Application
        {
            T application = (T) Activator.CreateInstance(typeof(T));
            application.Init();
            var forms = application.GetDefaultApplicationForms();
            application.Forms = new List<Form>(forms.Length);
            foreach (var form in forms)
            {
                if(form is IObserverForm)
                    ((IObserverForm)form).ObserveFormUpdate(null, application.ProjectInfo, application.ProjectInfo);
                application.Forms.Add(form);
            }
            // Create Status History, add first entry denoting creation
            application.StatusHistory = new List<ApplicationStatusLogItem>()
            {
                new ApplicationStatusLogItem()
                {
                    Status = EApplicationStatus.Fresh,
                    DateTime = DateTime.Now
                }
            };
            return application;
        }
        
        public int GetFormIdByType(Type type)
        {
            foreach (var form in GetOrderedForms())
                if (form.GetType() == type)
                    return form.Id;
            return -1;
        }

        public bool AllFormsSubmitted()
        {
            foreach (var form in GetOrderedForms())
                if (!form.IsAssigned && form.IsRequired)
                    return false;
            return true;
        }

        /// <summary>
        /// An application has the ability to review itself. This is sort of an inbetween between
        /// the things we can't validation with just property validation,
        /// and the things that we should make sure are correct.
        /// </summary>
        /// <returns>List of spotted errors within an application</returns>
        public virtual IList<ValidationException> Review()
        {
            return new ValidationException[0];
        }

        public override string ToString()
        {
            return $"{this.Id}_{this.DisplayName} {ProjectInfo.ToString()}";
        }

        /// <summary>
        /// Returns false if application cannot be finalized due to all forms not being
        /// submitted
        /// </summary>
        /// <returns></returns>
        public virtual bool SubmitAndFinalize()
        {
            // There can be no unassigned forms
            foreach (var form in GetOrderedForms())
            {
                if (form.IsRequired && !form.IsAssigned)
                    return false;
            }
            bool isIncomplete = HasStatus(EApplicationStatus.DeemedIncomplete);
            // There can be no warnings within the review. Also the application cannot be any step past submitted or resubmitted
            if (Review().Count > 0 ||
                (!isIncomplete ? CurrentStatusLog.Status > EApplicationStatus.Submitted : CurrentStatusLog.Status >= EApplicationStatus.Resubmitted))
                return false;
            foreach (var form in GetOrderedForms())
                if (form is SignatureForm)
                    ((SignatureForm)form).Signatures.ForEach(x => x.IsSignatureFinalized = true);
            StatusHistory.Add(ApplicationStatusLogItem.FactoryCreate(isIncomplete ? EApplicationStatus.Resubmitted : EApplicationStatus.Submitted));
            CanEdit = false;
            return true;
        }

        /// <summary>
        /// Returns whether a certain status has been logged
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool HasStatus(EApplicationStatus status)
        {
            return StatusHistory.Any(x => x.Status == status);
        }

        public virtual IEnumerable<string> NextSteps()
        {
            return null;
        }
    }
}
 