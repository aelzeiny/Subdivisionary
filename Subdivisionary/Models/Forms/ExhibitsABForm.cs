using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class ExhibitsABForm : UploadableFileForm
    {
        public override string DisplayName => "Exhibits A & B";

        public static readonly string EXHIBIT_A_KEY = "exhibitsAId";
        public static readonly string EXHIBIT_B_KEY = "exhibitsBId";
        public static readonly string EXHIBITS_DIRECTORY = "Exhibits";

        public static readonly string SAMPLE_EXHIBITS_A_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Exhibit%20A%20Example.pdf";
        public static readonly string SAMPLE_EXHIBITS_B_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Exhibit%20B%20Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, EXHIBIT_A_KEY, EXHIBITS_DIRECTORY, "Exhibit A"), 
            new FileUploadProperty(this.Id, EXHIBIT_B_KEY, EXHIBITS_DIRECTORY, "Exhibit B") 
        };
    }
}