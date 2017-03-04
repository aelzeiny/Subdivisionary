using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class OwnerList : SerializableList<OwnerInfo>
    {
    }

    public class OwnerInfo
    {
        [Required]
        [Display(Name = "Owner Name(s)")]
        public string OwnerName { get; set; }
    }
}