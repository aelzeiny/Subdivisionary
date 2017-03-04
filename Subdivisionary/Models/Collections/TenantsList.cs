using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class TenantsList : SerializableList<TenantInfo>
    {
    }

    public class TenantInfo
    {
        [Required]
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        [Required]
        [Display(Name = "Check this box if the tentant intends to purchase their respective unit. This will add an additional form to your application.")]
        public bool TenantIntentToPurchase { get; set; }

        [Required]
        [Display(Name = "Occupied Unit #")]
        public string Unit { get; set; }
    }
}