using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class EcpInfo: BypassInfo
    {
        [Required]
        public int ResidentialUnits { get; set; }
        [Required]
        public int CommercialUnits { get; set; }
    }
}