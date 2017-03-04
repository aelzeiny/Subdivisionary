using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class EcpOverviewForm : Form
    {
        public override string DisplayName => "ECP Overview";

        [RequiredTrue]
        [Display (Name = "The property owner(s) has/have read the above overview, and agrees that he/she/they qualify/qualifies for this application.")]
        public bool UserAgreesToEcpTerms { get; set; }

    }
}