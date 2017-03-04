using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class CheckList : SerializableList<CheckInfo>
    {
    }
    
    public class CheckInfo
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [DisplayName("Check Number")]
        [Required]
        public string CheckNumber { get; set; }

        [DisplayName("Routing Number")]
        [Required]
        public string RoutingNumber { get; set; }

        [DisplayName("Account Number")]
        [Required]
        public string AccountNumber { get; set; }
    }
}