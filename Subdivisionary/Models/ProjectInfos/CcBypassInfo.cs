﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    /// <summary>
    /// For Condo-Conversion Bypass Applications
    /// </summary>
    public class CcBypassInfo : ExtendedProjectInfo
    {
        [DisplayName("Number of Units")]
        public int NumberOfUnits { get; set; }

        public CcBypassInfo()
        {
            NumberOfUnits = 2;
        }
    }
}