using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class Report3R : UploadableFileForm
    {
        public override string DisplayName => "3R Report";

        public static readonly string SAMPLE_REPORT3R_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/DBI%203R%20Report%20Example.pdf";

        public static readonly string REPORT3R_KEY = "ThreeRReportId";
        public static readonly string REPORT3R_DIRECTORY_KEY = "3R Report";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Issuance")]
        public DateTime? Report3RIssuedDate { get; set; }
        
        [Required]
        [Display(Name = "Family Dwelling Size")]
        public int FamilyDwellingSize { get; set; }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, REPORT3R_KEY, REPORT3R_DIRECTORY_KEY, "3R Report")
        };
    }
}