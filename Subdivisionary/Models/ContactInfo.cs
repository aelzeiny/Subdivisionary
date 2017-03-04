using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models
{
    [ComplexType]
    public class ContactInfo
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        [Display(Name = "Address Line 2")]
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

        public ContactInfo()
        {
            Name = Email = AddressLine1 = AddressLine2 = City = State = Zip = "";
        }
    }
}