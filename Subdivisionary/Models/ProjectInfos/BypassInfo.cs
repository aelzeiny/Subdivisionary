using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class BypassInfo : ExtendedProjectInfo
    {
        [Required]
        public int TenantOccupiedUnits { get; set; }
    }
}