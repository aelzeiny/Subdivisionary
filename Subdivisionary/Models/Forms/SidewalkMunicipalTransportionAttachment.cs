using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkMunicipalTransportionAttachment : Form, ICollectionForm
    {
        public override string DisplayName => "MTA Attachment";

        public static readonly string MTA_TASC_COLL = "mtaTaskCollId";

        [Required]
        [Display(Name = "1. Has MTA reviewed your plans?")]
        public bool HasMtaReviewedPlans { get; set; }

        public NameSectionDateList MtaNameSectionDateList { get; set; }

        [Required]
        [Display(Name = "2. Has TASC reviewed your plans?")]
        public bool HasTascReviewedPlans { get; set; }

        [RequiredOnlyIfTascHearing]
        [Display(Name = "Date Heard by TASC")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MtaDateOfTascHearing { get; set; }

        [RequiredOnlyIfTascHearing]
        [Display(Name = "Date Approved by TASC")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MtaDateOfTascApproval { get; set; }


        [Required]
        [Display(Name = "3. Will your proposed work change street parking?")]
        public bool WillChangeStreetParking { get; set; }

        [RequiredOnlyIfParkingChanged]
        [Display(Name = "Spots Removed")]
        public int? ParkingSpotsRemoved { get; set; }

        [RequiredOnlyIfParkingChanged]
        [Display(Name = "List proposed curb color (red, blue, green, yellow, etc.")]
        public string ProposedCurbColor { get; set; }

        public string[] Keys => new[] {MTA_TASC_COLL};

        public SidewalkMunicipalTransportionAttachment()
        {
            MtaNameSectionDateList = new NameSectionDateList();
        }

        public ICollectionAdd GetListCollection(string key)
        {
            return MtaNameSectionDateList;
        }

        public object GetEmptyItem(string key)
        {
            return new NameSectionDateInfo();
        }


        public class RequiredOnlyIfTascHearing : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext != null && value == null && ((SidewalkMunicipalTransportionAttachment)validationContext.ObjectInstance).HasTascReviewedPlans)
                    return new ValidationResult("Missing information for TASC Approval");
                return ValidationResult.Success;
            }
        }
        public class RequiredOnlyIfParkingChanged : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext != null && value == null && ((SidewalkMunicipalTransportionAttachment)validationContext.ObjectInstance).WillChangeStreetParking)
                    return new ValidationResult("Missing information for TASC Approval");
                return ValidationResult.Success;
            }
        }
    }
}