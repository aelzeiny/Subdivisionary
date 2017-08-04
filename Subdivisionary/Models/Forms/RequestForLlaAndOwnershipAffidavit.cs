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

        public static readonly string SAMPLE_REQUEST_LLA_URL = "~/Samples/Request for LLA and Affidavit of Ownership Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, REQUEST_LLA_KEY, REQUEST_LLA_DIRECTORY, "Request for LLA")
        };
    }
}