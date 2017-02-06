using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Validation
{
    public class UnitsAddUpToTotalValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as CcEcpInfo;
            if(obj != null && obj.CommercialUnits + obj.ResidentialUnits != obj.NumberOfUnits)
                return new ValidationResult("Residential and Commercial units do not add up to total units");
            return ValidationResult.Success;
        }
    }
}