using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Dtos
{
    /// <summary>
    /// Data Transfer Object for ContactInfo.
    /// </summary>
    public class ContactInfoDto
    {
        /// <summary>
        /// Specified Contact Name
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Specified Email Address
        /// </summary>
        [Required]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Optional 10-digit Phone #
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Line 1 of Contact Address
        /// </summary>
        [Required]
        [StringLength(255)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Optional Line 2 of Contact Address
        /// (i.e: Floor, Unit, Apt, etc.)
        /// </summary>
        [StringLength(255)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Contact Residing City
        /// </summary>
        [Required]
        [StringLength(255)]
        public string City { get; set; }

        /// <summary>
        /// Contact State
        /// </summary>
        [Required]
        [StringLength(255)]
        public string State { get; set; }

        /// <summary>
        /// Contact Zip Code
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Zip { get; set; }
    }
}