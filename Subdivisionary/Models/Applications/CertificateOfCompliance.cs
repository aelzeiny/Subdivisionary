using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
   
    public class CertificateOfCompliance : Application
    {
        protected override void Init()
        {
            ProjectInfo = new ExtendedProjectInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new ApplicationCheckForm(),
                new TitleReportForm(),
                new PhotographForm(),
                new GrantDeedForm(),
                new ClosureCalcsForm()
            };
        }

        public override string DisplayName => "Certificate of Compliance";
    }
}