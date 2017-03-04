using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    public class PermitList : SerializableList<PermitInfo>
    {
    }

    public class PermitInfo
    {
        [Display(Name = "Permit #")]
        [Range(0, int.MaxValue)]
        public int PermitId { get; set; }

        public PermitInfo()
        {
            PermitId = 0;
        }
    }
}