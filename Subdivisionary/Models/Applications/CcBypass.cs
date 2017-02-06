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
                new ApplicationCheckForm(),
                new TentativeMapForm(),
                new TitleReportForm(),
                new GrantDeedForm()
                //new OtherRecordedMapsOrBlockResearch(),
                //new BuildingHistoryForm(),
                //new CertificateOfFinalCompletionForm(),
                //new Report3RForm(),
                //new ProofOfOwnerOccupancyForm()
            };
        }

        public override string DisplayName => "Two-Unit Bypass";
    }
}