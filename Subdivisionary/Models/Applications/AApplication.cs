using Subdivisionary.Models.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public abstract class AApplication
    {
        /**
         * This stores the id of this class
         */
        public int Id { get; set; }

        /**
         * This stores the id of the corresponding Project Info field
         * This is using EntityFrameworks' Convention where BasicProjectInfo & ProjectInfoId are
         * automatically linked together.
         */
        public int ProjectInfoId { get; set; }
        [Required]
        public virtual BasicProjectInfo ProjectInfo { get; set; }


        /**
         * Each Application has one Applicant. This stores the applicant's ID & Applicant.
         * This is using EntityFrameworks' Convention where ApplicantID & Applicant are
         * automatically linked together.
         */
        public int ApplicantId { get; set; }
        [Required]
        public Applicant Applicant { get; set; }

        /**
         * Represents whether or not an application has been submitted yet
         */
        public bool IsSubmitted { get; set; }

        /**
         * Returns a List of Forms included in a given applications. This comes in handy
         * later.
         */
        public IForm[] Forms
        {
            get
            {
                IForm[] formOrder = GetForms();
                IForm[] answer = new IForm[formOrder.Length + 2];
                answer[0] = ProjectInfo;
                Array.ConstrainedCopy(formOrder, 0, answer, 1, formOrder.Length);
                answer[answer.Length - 1] = AdditionalDocumentsForm;
                return answer;
            }
        }

        protected abstract IForm[] GetForms();

        public abstract string DisplayName { get; }


        /**
         * Here we declare All of the Possible forms in all applications.
         * I know this is an unnatural object-oriented approach, but we do this for
         * because Entity Framework cannot handle object-oriented complex-types with shared columns
         * very well. Note the Convention: All IForms must be named after the .PropertyName string
         */

        public AdditionalDocumentsForm AdditionalDocumentsForm { get; set; }
        /*public virtual ApplicationCheckForm ApplicationCheckForm { get; set; }
        public virtual TitleReportForm TitleReportForm { get; set; }
        public virtual GrantDeedForm GrantDeedForm { get; set; }
        public virtual ClosureCalcsForm ClosureCalcsForm { get; set; }
        public virtual PhotographForm PhotographForm { get; set; }*/

        protected AApplication()
        {
            AdditionalDocumentsForm = new AdditionalDocumentsForm();
        }

        public void UpdateForm(IForm editApp)
        {
            this.GetType().GetProperty(editApp.PropertyName).SetValue(this, editApp);
        }
    }
}
 