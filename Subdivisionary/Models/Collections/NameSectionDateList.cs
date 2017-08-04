using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    public class NameSectionDateList : SerializableList<NameSectionDateInfo>
    {
    }

    public class NameSectionDateInfo
    {
        [Required]
        [Display(Name = "Name of Reviewer")]
        public string NameOfReviewer { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        [Display(Name = "Date Reviewed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateReviewed { get; set; }
    }
}