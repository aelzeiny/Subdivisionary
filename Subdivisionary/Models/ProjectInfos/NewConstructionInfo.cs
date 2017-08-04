using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class NewConstructionInfo : CcEcpInfo
    {
        [Required]
        [Display(Name = "This subdivision creates a vertical subdivision? (if checked then please show on tentative map)")]
        public bool CreatesVerticalSubdivision { get; set; }

        [Required]
        [Display(Name = "This subdivision includes an existing building/dwelling? (if checked then please show on tentative map)")]
        public bool HasExistingBuilding { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "New Construction Projects must be greater than 0 units.")]
        public override int NumberOfUnits { get; set; }
    }
}