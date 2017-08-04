using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class ClosureCalcsForm : UploadableFileForm
    {
        public override string DisplayName => "Electronic Closure Calculations";

        public static readonly string CLOSURE_CALCS_KEY = "closureCalcsId";
        public static readonly string CLOSURE_CALCS_DIRECTORY = "Closure Calcs";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, CLOSURE_CALCS_KEY, CLOSURE_CALCS_DIRECTORY, "Closure Calculations")
        };

        public ClosureCalcsForm()
        {
            IsRequired = false;
        }
    }
}