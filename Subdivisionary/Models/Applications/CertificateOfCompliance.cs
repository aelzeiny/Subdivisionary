using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
   
    public class CertificateOfCompliance : AApplication
    {
        public virtual ApplicationCheckForm ApplicationCheckForm { get; set; }
        public virtual TitleReportForm TitleReportForm { get; set; }
        public virtual GrantDeedForm GrantDeedForm { get; set; }
        public virtual ClosureCalcsForm ClosureCalcsForm { get; set; }
        public virtual PhotographForm PhotographForm { get; set; }

        private CertificateOfCompliance()
        {
            TitleReportForm = new TitleReportForm();
            ApplicationCheckForm = new ApplicationCheckForm();
            PhotographForm = new PhotographForm();
            GrantDeedForm = new GrantDeedForm();
            ClosureCalcsForm = new ClosureCalcsForm();
        }

        public static CertificateOfCompliance Create()
        {
            return new CertificateOfCompliance()
            {
                ProjectInfo = new ExtendedProjectInfo()
            };
        }

        protected override IForm[] GetForms()
        {
            return new IForm[]
            {
                ApplicationCheckForm,
                TitleReportForm,
                PhotographForm,
                GrantDeedForm,
                ClosureCalcsForm
            };
        }

        public override string DisplayName => "Certificate of Compliance";
    }
}