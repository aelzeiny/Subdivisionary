using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class DbiPhysicalInspection : UploadableFileForm
    {
        public override string DisplayName => "DBI Proof of Inspection";

        public static readonly string RECEIPT_KEY = "receiptId";
        public static readonly string INSPECTION_KEY = "inspectionId";
        public static readonly string CFC_KEY = "cfcId";

        public static readonly string DBI_DIRECTORY = "Physical DBI Inspection";

        public static readonly string SAMPLE_RECEIPT_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/DBI%20Receipt%20for%20Inspection%20Example.pdf";
        public static readonly string SAMPLE_INSPECTION_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/DBI%20Inspection%20Report%20Example.pdf";
        public static readonly string SAMPLE_CFC_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/DBI%20CFC%20Example.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, RECEIPT_KEY, DBI_DIRECTORY, "Receipt", true, true),
            new FileUploadProperty(this.Id, INSPECTION_KEY, DBI_DIRECTORY, "Inspection", true, false),
            new FileUploadProperty(this.Id, CFC_KEY, DBI_DIRECTORY, "Final CFC", true, false),
        };
    }
}