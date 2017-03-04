﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    /// <summary>
    /// For Coc & LLA Apps
    /// </summary>
    public class CocAndLlaProjectInfo : ExtendedProjectInfo
    {
        public ContactInfo DeveloperContactInfo { get; set; }

        [Required]
        [Display(Name = "Number of Existing Lots")]
        public int NumOfExisitingLots { get; set; }

        public bool OwnerAndLandDevContactAreSame { get; set; }

        public CocAndLlaProjectInfo()
        {
            DeveloperContactInfo = new ContactInfo();
            OwnerAndLandDevContactAreSame = false;
        }
    }
}