using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class LocationWidthList : SerializableList<LocationWidthInfo>
    {

    }

    public class LocationWidthInfo
    {
        [Required]
        [Display(Name="Location (intersection corner or fronting address)")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Width in Feet")]
        public string WidthInFeet { get; set; }
    }
}