using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class DeveloperExtendedProjectInfo : ExtendedProjectInfo
    {
        [Required]
        public ContactInfo DeveloperContactInfo { get; set; }

        public DeveloperExtendedProjectInfo()
        {
            DeveloperContactInfo = new ContactInfo();
        }
    }
}