using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class RecordOfSurvey : AApplication
    {
        public virtual ApplicationCheckForm ApplicationCheckForm { get; set; }
        public virtual TitleReportForm TitleReportForm { get; set; }
        public virtual ClosureCalcsForm ClosureCalcsForm { get; set; }
        private RecordOfSurvey()
        {
            ApplicationCheckForm = new ApplicationCheckForm();
            ClosureCalcsForm = new ClosureCalcsForm();
            TitleReportForm = new TitleReportForm();
        }


        public static RecordOfSurvey Create()
        {
            return new RecordOfSurvey()
            {
                ProjectInfo = new BasicProjectInfo()
            };
        }

        protected override IForm[] GetForms()
        {
            return new IForm[]
            {
                ApplicationCheckForm,
                TitleReportForm,
                ClosureCalcsForm
            };
        }

        public override string DisplayName => "Record of Survey";
    }
}