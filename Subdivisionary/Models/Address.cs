using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    [ComplexType]
    public class Address
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        
        [StringLength(255)]
        [Display(Name = "Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string State { get; set; }

        [Required]
        [StringLength(255)]
        public string Zip { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(AddressLine2))
                return string.Format("{0},\n{1}, {2} {3}", AddressLine1, City, State, Zip);
            return string.Format("{0}\n{1},\n{2}, {3} {4}", AddressLine1, AddressLine2, City, State, Zip);
        }
    }
}