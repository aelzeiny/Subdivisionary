﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Dtos
{
    public class ContactInfoDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
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
    }
}