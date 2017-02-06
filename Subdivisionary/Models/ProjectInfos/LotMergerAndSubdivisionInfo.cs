using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class LotMergerAndSubdivisionInfo : CocAndLlaProjectInfo
    {
        [Required]
        [Display(Name = "Number of Proposed Lots")]
        public int NumOfProposedLots { get; set; }
    }
}