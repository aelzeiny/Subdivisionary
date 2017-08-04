using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkLegislationGuidelinesForm : Form
    {
        public override string DisplayName => "Sidewalk Legislation Guidlines";

        [RequiredTrue]
        [Display(Name = "The individual affirms by checking this box that he/she/they have read and understood the guidelines to qualify and apply for this application.")]
        public bool ReadAndAffirmSidewalkApp { get; set; }
    }
}