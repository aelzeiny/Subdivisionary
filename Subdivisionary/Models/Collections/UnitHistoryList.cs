using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{

    [ComplexType]
    public class UnitHistoryList : SerializableList<UnitHistoryInfo>
    {
    }


    public class UnitHistoryInfo
    {
        [Required]
        public int YearRangeStart { get; set; }
        [Required]
        public int YearRangeEnd { get; set; }

        [Required]
        public string Occupants { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Rent { get; set; }

        [Required]
        public string ReasonForTermination { get; set; }
    }

}