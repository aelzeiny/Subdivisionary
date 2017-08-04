using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class NewConstructionReference : Application
    {
        protected override void Init()
        {
            ProjectInfo = new NewConstructionInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new OwnerForm(),
                new TentativeMapForm(),
                new TitleReportForm(),
                new GrantDeedForm(),
                //new PreviousLandUseForm(),
                new OwnersReleaseOfInterestInCommonAreas(),
                new NeighborhoodNotificationForm(),
                new PhotographForm(),
                new PropMFindingsForm(),
                new DbiInspectionRequirementsForm(),
                new SignedDcpMotionForm(),
                //new NoticeOfSpecialRestrictions() {IsRequired = false },
                new Report3R(),
                //new NewConstructionBuildingPermits(){IsRequired = false }

            };
        }

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.NewConstruction;

        public override string DisplayName => "New Construction";
    }
}