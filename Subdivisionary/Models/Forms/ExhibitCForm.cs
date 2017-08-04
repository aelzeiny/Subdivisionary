using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class ExhibitCForm : UploadableFileForm
    {
        public override string DisplayName => "Exhibits C";

        public static readonly string EXHIBIT_A_KEY = "exhibitsAId";
        public static readonly string EXHIBIT_B_KEY = "exhibitsBId";
        public static readonly string EXHIBITS_DIRECTORY = "Exhibits";

        public static readonly string SAMPLE_EXHIBITS_A_URL = "~/Samples/Exhibit A Example.pdf";
        public static readonly string SAMPLE_EXHIBITS_B_URL = "~/Samples/Exhibit B Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, EXHIBIT_A_KEY, EXHIBITS_DIRECTORY, "Exhibit A"),
            new FileUploadProperty(this.Id, EXHIBIT_B_KEY, EXHIBITS_DIRECTORY, "Exhibit B")
        };
    }
}