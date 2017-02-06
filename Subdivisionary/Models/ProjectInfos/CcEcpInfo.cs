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
}