using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class EcpGuidelinesForm : Form
    {
        public override string DisplayName => "ECP Guidelines";

        [RequiredTrue]
        [Display(Name = "The property owner(s) has/have read the above guidelines, and agrees that he/she/they qualify/qualifies for this application.")]
        public bool UserAgreesToEcpGuidelines { get; set; }
    }
}