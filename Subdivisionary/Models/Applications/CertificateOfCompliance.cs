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
                new ApplicationCheckForm(),
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

        public override IList<ValidationResult> Review()
        {
            IList<ValidationResult> answer = new List<ValidationResult>();
            // Ensure 40% of applicants have signed the intent to purchase form
            var projectInfo = (CcEcpInfo) ProjectInfo;
            var requiredTenantAgreements = Math.Round(.4f * projectInfo.NumberOfUnits);
            int numberofSignedTenants =
                Forms.Count(x => (x is TenantIntentToPurchase || x is TenantIntentToAcceptOfferOfLtl) && x.IsAssigned);
            if (numberofSignedTenants < requiredTenantAgreements)
            {
                answer.Add(new ValidationResult($"Currently only {requiredTenantAgreements} have either signed their intent to purchase" +
                                                " the unit or accept their offer of lifetime lease. " +
                                                $"This application requires at least {requiredTenantAgreements}"));
            }

            return answer;
        }

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.LotlineAdjustment;

        public override string DisplayName => "Certificate of Compliance";
    }
}