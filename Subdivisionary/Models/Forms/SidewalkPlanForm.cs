using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkPlanForm : UploadableFileForm
    {
        public override string DisplayName => "Sidewalk Plans";

        public static readonly string SIDEWALK_KEY = "plansUploadId";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, SIDEWALK_KEY, "", "Sidewalk Plans") 
        };
    }
}