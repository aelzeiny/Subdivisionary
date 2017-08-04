using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class ParcelOrFinalMapGuidelines : Form
    {
        public override string DisplayName => "Parcel/Final Map Guidlines";

        [RequiredTrue]
        [Display(Name = "The individual affirms by checking this box that he/she/they have read and understood the guidelines to qualify and apply for this application.")]
        public bool ReadAndAffirmPmAndFmApp { get; set; }
    }
}