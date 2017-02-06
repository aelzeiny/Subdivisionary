using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Validation
{
    public class TitleCompanyOtherValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var form = ((TitleReportForm)validationContext.ObjectInstance);
            if(form.TitleCompany == TitleCompany.Other && ((string)value).IsNullOrWhiteSpace())
                return new ValidationResult("Please specify the Title Company");
            return ValidationResult.Success;
        }
    }
}