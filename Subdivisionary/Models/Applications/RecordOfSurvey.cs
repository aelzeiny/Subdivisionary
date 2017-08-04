using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new TitleReportForm() {IsRequired = false}, 
                new TentativeMapForm(),
                new GrantDeedForm(),  
                new ClosureCalcsForm(),
                new RecordedMapsOnBlock()
            };
        }
        
        public override EFeeSchedule PaymentSchedule => EFeeSchedule.RecordOfSurvey;

        public override string DisplayName => "Record of Survey";
    }
}