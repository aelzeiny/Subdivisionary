using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.Models;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.DAL
{
    public class RequiredUpload : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
            /*
            var val = (value as FileUploadProperty);
            if (val != null)
            {
                var files = val;
                if (files.Count == 0)
                    return new ValidationResult(val.IsSingleUpload ? "Please upload a file" : "Please upload the necessary files");
                foreach (var file in files)
                {
                    if(file.IsNullOrWhiteSpace())
                        return new ValidationResult("File Path cannot be empty");
                    if (file.StartsWith("\\~") || file.StartsWith("/~"))
                        return new ValidationResult("Improper file path");
                }
                return ValidationResult.Success;
            }
            throw new NotSupportedException("Cannot validate this type");*/
        }
    }
}