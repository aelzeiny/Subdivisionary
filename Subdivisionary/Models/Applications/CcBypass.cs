using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class CcBypass : Application
    {
        protected override void Init()
        {
            ProjectInfo = new CcBypassInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new OwnerForm(),
                new CoverLetterForm(),
                new ApplicationCheckForm(),
                new TentativeMapForm(),
                new TitleReportForm(),
                new GrantDeedForm(),
                new BuildingHistory(),
                new UnitHistoryForm(),
                new UnitHistoryForm(),
                new RecordedMapsOnBlock(),
                new DbiPhysicalInspection(), 
                new Report3R(),
                new ProofOfOwnerOccupancy(), 
                new OwnersReleaseOfInterestInCommonAreas(), 
                new OwnersAffidavitOfTentantEvictions(),
            };
        }

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.Conversions;

        public override string DisplayName => "Two-Unit Bypass";
    }
}