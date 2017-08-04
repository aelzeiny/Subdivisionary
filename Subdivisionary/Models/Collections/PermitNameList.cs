using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class PermitNameList : SerializableList<PermitNameInfo>
    {
    }

    public class PermitNameInfo
    {
        [Display(Name="Reviewer Name")]
        public string ReviewerName { get; set; }

        [Display(Name = "Permit No")]
        public string PermitNo { get; set; }
    }
}