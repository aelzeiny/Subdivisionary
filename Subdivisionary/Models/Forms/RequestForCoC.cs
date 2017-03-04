using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class RequestForCoc : UploadableFileForm
    {
        public override string DisplayName => "Request for Certificate of Compliance";

        public static readonly string REQUEST_COC_KEY = "requestCocId";
        public static readonly string REQUEST_COC_DIRECTORY = "Request for CoC";

        public static readonly string SAMPLE_REQUEST_COC_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Request%20for%20Certificate%20of%20Compliance%20Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, REQUEST_COC_KEY, REQUEST_COC_DIRECTORY, "Request for Coc")
        };
    }
}