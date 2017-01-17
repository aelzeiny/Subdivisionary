using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class RecordOfSurvey : Application
    {
        protected override void Init()
        {
            ProjectInfo = new BasicProjectInfo();
        }

        protected override Type[] GetDefaultApplicationForms()
        {
            return new Type[]
            {
                typeof(ApplicationCheckForm),
                typeof(TitleReportForm),
                typeof(ClosureCalcsForm)
            };
        }

        public override string DisplayName => "Record of Survey";
    }
}