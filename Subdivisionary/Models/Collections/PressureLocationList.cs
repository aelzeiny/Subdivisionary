using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class PressureLocationList : SerializableList<PressureLocationInfo>
    {
    }

    public class PressureLocationInfo
    {
        [Required]
        [Display(Name = "Pressure Type")]
        public PressureType PressureType { get; set; }

        [Required]
        [Display(Name = "Location (intersection corner or frontine address)")]
        public string Location { get; set; }
    }

    public enum PressureType
    {
        [Display(Name = "Low Pressure")]
        LowPressure,
        [Display(Name = "High Pressure")]
        HighPressure
    }

}