using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class TenantContactsList : SerializableList<TenantContactInfo>
    {
    }

    public class TenantContactInfo
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Contact")]
        public DateTime? DateOfContact { get; set; }

        [Required]
        [Display(Name = "Date of Contact")]
        public string InteractionWithTenants { get; set; }
    }
}