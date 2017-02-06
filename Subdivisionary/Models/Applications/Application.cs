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

        /**
         * Display name of application
         */
        public abstract string DisplayName { get; }
        public string DirectoryName => this.Id + "_" + this.DisplayName;

        protected abstract Form[] GetDefaultApplicationForms();

        protected abstract void Init();

        protected Application()
        {
            SharedRequests = new EmailList();
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
        public virtual void FormUpdated(IForm before, IForm after)
        {
            foreach (var form in Forms)
            {
                var observer = form as IObservableForm;
                if (observer != null)
                    observer.UpdateForm(before, after);
            }
        }

        /// <summary>
        /// Factory Design Pattern used to create a new Application. The constructor is reserved
        /// for Entity Framework by practice. Here we can create forms using their constructor and modify
        /// their variables (such as requirement).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Application FactoryCreate<T>() where T : Application
        {
            Application application = (Application) Activator.CreateInstance(typeof(T));
            application.Init();
            var forms = application.GetDefaultApplicationForms();
            application.Forms = new List<Form>(forms.Length);
            foreach (var form in forms)
                application.Forms.Add(form);
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

        public override string ToString()
        {
            return string.Format("{0}_{1} {2}", this.Id, this.DisplayName, ProjectInfo.ToString());
        }
    }
}
 