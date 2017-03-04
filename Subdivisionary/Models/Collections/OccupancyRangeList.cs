using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    public class OccupancyRangeList : SerializableList<OccupancyRangeInfo>
    {
    }

    public class OccupancyRangeInfo
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date of Occupancy")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date of Occupancy")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Occupants (owners and/or Tenants)")]
        public string Occupants { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Rent")]
        public decimal Rent { get; set; }

        [Required]
        [Display(Name = "Reason for Termination")]
        public string ReasonForTermination { get; set; }
    }
}