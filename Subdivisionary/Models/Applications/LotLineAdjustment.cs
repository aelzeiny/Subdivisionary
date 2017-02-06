using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class LotLineAdjustment : Application
    {
        protected override void Init()
        {
            ProjectInfo = new CocAndLlaProjectInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
            };
        }

        public override string DisplayName => "Lot-Line Adjustment";
    }
}