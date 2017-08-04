using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
   
    public class CertificateOfCompliance : Application
    {
        protected override void Init()
        {
            ProjectInfo = new CocAndLlaProjectInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new OwnerForm(),
                new RequestForCoc(),
                new ExhibitsABForm(),
                new TitleReportForm(),
                new PhotographForm(),
                new GrantDeedForm(),
                new ClosureCalcsForm() {IsRequired = false},
                new PropMFindingsForm()
            };
        }

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.LotlineAdjustment;

        public override string DisplayName => "Certificate of Compliance";
    }
}