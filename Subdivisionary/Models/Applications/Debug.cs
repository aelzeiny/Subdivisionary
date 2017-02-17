using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class DebugApplication : Application
    {
        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new OwnerForm(), 
                new OwnersAffidavitOfTentantEvictions(),
                new NeighborhoodNotificationForm(),
                new PropMFindingsForm(),
                new CoverLetterForm(),
                new TentativeMapForm(),
                new RecordedMapsOnBlock()
            };
        }

        protected override void Init()
        {
            ProjectInfo = new CcEcpInfo();
        }

        public override string DisplayName => "Simplest Application";
    }
}