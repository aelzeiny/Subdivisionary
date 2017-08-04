using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class SidewalkLegislation : Application
    {
        public override string DisplayName => "Sidewalk Legislation";

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.SidewalkLegislation;
        
        protected override void Init()
        {
            ProjectInfo = new ExtendedProjectInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new SidewalkLegislationGuidelinesForm(), 
                new CoverLetterSidewalkLegislationForm(),
                new SidewalkPlanForm(),
                new SidewalkDcpAttachment(),
                new SidewalkBsmAttachment(),
                new SidewalkDpwHydraulicAttachment(),
                new SidewalkFireDepartmentAttachment(),
                new SidewalkMunicipalTransportionAttachment()
            };
        }
    }
}