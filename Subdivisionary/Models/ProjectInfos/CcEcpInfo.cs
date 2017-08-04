using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.ProjectInfos
{
    /// <summary>
    /// For, you guessed it, Condo-Conversion ECP Projects
    /// </summary>
    public class CcEcpInfo: CcBypassInfo
    {
        [Required]
        [UnitsAddUpToTotalValidation]
        [Display(Name = "Number of Residential units")]
        public int ResidentialUnits { get; set; }
        [Required]
        [UnitsAddUpToTotalValidation]
        [Display(Name = "Number of Commercial units")]
        public int CommercialUnits { get; set; }
    }

    /// <summary>
    /// Ensures that Total Units = Residential Units + Commercial Units for ECP Infos & all subtypes
    /// </summary>
    public class UnitsAddUpToTotalValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as CcEcpInfo;
            if (obj != null && obj.CommercialUnits + obj.ResidentialUnits != obj.NumberOfUnits)
                return new ValidationResult("Residential and Commercial units do not add up to total units");
            return ValidationResult.Success;
        }
    }
}