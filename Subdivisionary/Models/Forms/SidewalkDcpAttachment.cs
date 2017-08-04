using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkDcpAttachment : Form
    {
        public override string DisplayName => "DCP Attachment";
        
        [Required]
        [Display(Name = "1. Has the Department of City Planning reviewed your project?")]
        public bool DcpReviewedProject { get; set; }

        [RequiredOnlyIfDcpReview]
        [Display(Name = "Date Of Review")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DcpDateOfReview { get; set; }

        [Display(Name = "City Planning Case No. (if avaliable)")]
        public string DcpCaseNoReview { get; set; }

        [RequiredOnlyIfDcpReview]
        [Display(Name = "DCP Reviewer Name")]
        public string DcpReviewerName { get; set; }

        [Display(Name = "2. Does the sidewalk change have General Plan Referral Approval?")]
        public bool DcpHaveGeneralReferralApproval { get; set; }

        [RequiredOnlyIfGeneralReview]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Approval")]
        public DateTime? DcpDateOfReferralApproval { get; set; }

        [RequiredOnlyIfGeneralReview]
        [Display(Name = "City Planning Case No.")]
        public string DcpCaseNoReferralApproval { get; set; }


        [Display(Name = "3. Has the sidewalk change been environmentally cleared under the Califronia Environmental Quality Act")]
        public bool DcpCeqaClearance { get; set; }

        [RequiredOnlyIfCeqaReview]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Approval")]
        public DateTime? DcpDateOfCeqaApproval { get; set; }

        [RequiredOnlyIfCeqaReview]
        [Display(Name = "City Planning Case No.")]
        public string DcpCaseNoCeqaApproval { get; set; }

        public class RequiredOnlyIfDcpReviewAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext == null)
                    return ValidationResult.Success;
                if (value == null && ((SidewalkDcpAttachment)validationContext.ObjectInstance).DcpReviewedProject)
                    return new ValidationResult("Missing information for DCP Project Review");
                return ValidationResult.Success;
            }
        }
        public class RequiredOnlyIfGeneralReviewAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext == null)
                    return ValidationResult.Success;
                if (value == null && ((SidewalkDcpAttachment)validationContext.ObjectInstance).DcpHaveGeneralReferralApproval)
                    return new ValidationResult("Missing information for Referral Approval");
                return ValidationResult.Success;
            }
        }
        public class RequiredOnlyIfCeqaReviewAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext == null)
                    return ValidationResult.Success;
                if (value == null && ((SidewalkDcpAttachment)validationContext.ObjectInstance).DcpCeqaClearance)
                    return new ValidationResult("Missing information for CEQA Clearance");
                return ValidationResult.Success;
            }
        }
    }
}