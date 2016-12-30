using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class ExtendedProjectInfo : BasicProjectInfo
    {
        [Required]
        [Display(Name="Number of Units")]
        public int NumberOfUnits { get; set; }
        [Required]
        public ContactInfo FirmContactInfo { get; set; }

        public ExtendedProjectInfo()
        {
            FirmContactInfo = new ContactInfo();
        }
    }
}