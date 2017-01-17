using Subdivisionary.Models.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public abstract class Application
    {
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
        public Applicant Applicant { get; set; }
        public int ApplicantId { get; set; }

        /**
         * Represents whether or not an application has been submitted yet
         */
        public bool IsSubmitted { get; set; }

        /**
         * Display name of application
         */
        public abstract string DisplayName { get; }
        public string DirectoryName => this.Id + "_" + this.DisplayName;

        protected abstract Type[] GetDefaultApplicationForms();

        protected abstract void Init();

        public virtual IList<IForm> GetOrderedForms()
        {
            List<IForm> answer = new List<IForm>();
            answer.Add(this.ProjectInfo);
            answer.AddRange(this.Forms);
            return answer;
        }

        public static Application Create<T>() where T : Application
        {
            Application application = (Application) Activator.CreateInstance(typeof(T));
            application.Init();
            var forms = application.GetDefaultApplicationForms();
            application.Forms = new List<Form>(forms.Length);
            foreach (var form in forms)
                application.Forms.Add((Form)(Activator.CreateInstance(form)));
            return application;
        }

        public int GetFormIdByType(Type type)
        {
            foreach (var form in this.GetOrderedForms())
                if (form.GetType() == type)
                    return form.Id;
            return -1;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}({2}-{3})", this.Id, this.DisplayName, this.ProjectInfo.Block,
                this.ProjectInfo.Lot);
        }
    }
}
 