using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class RequestForLlaAndOwnershipAffidavit : UploadableFileForm
    {
        public override string DisplayName => "Request for Certificate of Compliance";

        public static readonly string REQUEST_LLA_KEY = "requestLlaId";
        public static readonly string REQUEST_LLA_DIRECTORY = "Request for LLA";

        public static readonly string SAMPLE_REQUEST_LLA_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Request%20for%20LLA%20and%20Affidavit%20of%20Ownership%20Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, REQUEST_LLA_KEY, REQUEST_LLA_DIRECTORY, "Request for LLA")
        };
    }
}