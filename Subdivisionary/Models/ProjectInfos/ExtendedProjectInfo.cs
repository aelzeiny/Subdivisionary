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
        public ContactInfo LandFirmContactInfo { get; set; }

        public ExtendedProjectInfo()
        {
            LandFirmContactInfo = new ContactInfo();
        }
    }
}