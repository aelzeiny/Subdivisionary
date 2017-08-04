﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class LocationHeightList : SerializableList<LocationHeightInfo>
    {
    }

    public class LocationHeightInfo
    {
        [Required]
        [Display(Name = "Location (intersection corner or fronting address)")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Height in Feet")]
        public string HeightInFeet { get; set; }
    }
}