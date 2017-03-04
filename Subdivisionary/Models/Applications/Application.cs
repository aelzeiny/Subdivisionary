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
         * Represents whether or not an application has been submitted yet
         */
        public bool IsSubmitted { get; set; }

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
        }

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

        public virtual IList<ValidationResult> Review()
        {
            return new ValidationResult[0];
        }

        public override string ToString()
        {
            return $"{this.Id}_{this.DisplayName} {ProjectInfo.ToString()}";
        }
    }
}
 