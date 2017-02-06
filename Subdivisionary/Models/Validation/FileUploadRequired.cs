using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Validation
{
    public class FileUploadRequired : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (((FileUploadList)value).Count == 0) ? new ValidationResult("Please upload a file") : ValidationResult.Success;
        }
    }
}