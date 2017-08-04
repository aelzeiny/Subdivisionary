using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class CoverLetterForm : UploadableFileForm
    {
        public override string DisplayName => "Cover Letter";

        public static readonly string COVER_LETTER_KEY = "coverLetterId";
        public static readonly string COVER_LETTER_DIRECTORY = "Cover Letter";

        public static readonly string SAMPLE_COVER_LETTER_URL = "~/Samples/Cover Letter Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, COVER_LETTER_KEY, COVER_LETTER_DIRECTORY, "Cover Letter"), 
        };
    }
}