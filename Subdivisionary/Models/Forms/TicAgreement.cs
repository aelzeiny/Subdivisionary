using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Subdivisionary.Models.Forms
{
    public class TicAgreement : UploadableFileForm
    {
        public override string DisplayName => "TIC Agreement";

        public static readonly string TIC_AGREEMENT_KEY = "ticAgreementId";
        public static readonly string SAMPLE_TIC_URL = "https://subdivisionaryblob.blob.core.windows.net:443/templates/TIC%20Agreement%20Example.pdf";

        public static readonly string TIC_AGREEMENT_DIRECTORY = "TIC Agreement";

        [Required]
        [PostApril152013]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Agreement")]
        public DateTime? TicAgreementDate { get; set; }

        [Required]
        [Display(Name = "Pages of Document with signatures and/or date")]
        public string TicPages { get; set; }
        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, TIC_AGREEMENT_KEY, TIC_AGREEMENT_DIRECTORY, "TIC Agreemeent")
        };
    }

    public class PostApril152013 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime) value;
            if(date.CompareTo(new DateTime(2013, 4, 15)) > 0)
                return new ValidationResult("Tenancy-In-Common Agreement must be established prior to April 15, 2013.");
            return ValidationResult.Success;
        }
    }
}