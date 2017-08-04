using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class DateNameList : SerializableList<DateNameInfo>
    {
    }

    public class DateNameInfo
    {
        [Required]
        [Display(Name = "Reviewer Name")]
        public string ReviewerName { get; set; }

        [Required]
        [Display(Name = "Date Reviewed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateReviewed { get; set; }
    }
}