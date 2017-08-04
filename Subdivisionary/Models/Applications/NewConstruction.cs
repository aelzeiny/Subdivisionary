using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class NewConstruction : Application
    {
        public override string DisplayName => "New Condominium Construction";

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.NewConstruction;


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
                // new PreviousLandUseForm(),
                new OwnersReleaseOfInterestInCommonAreas(), 
                new NeighborhoodNotificationForm(), 
                new PhotographForm(), 
                new PropMFindingsForm(), 
                new DbiInspectionRequirementsForm(), 
                // new DcpSignedMotionForm(), // <- UploadableFileForm
                new Report3R()
                // new NoticeOfSpecialRestrictions() {IsRequired = false },
                // new NewConstructionBuildingPermits(){IsRequired = false }
            };
        }
    }
}