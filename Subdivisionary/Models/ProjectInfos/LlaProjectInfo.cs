using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.ProjectInfos
{
    public class LlaProjectInfo : LotMergerAndSubdivisionInfo
    {
        [LlaProposedLotsCheck]
        [Range(0,1,ErrorMessage = "Lot-Line Adjustments cannot exceed 4 Units, please create a Parcel/Final Map Application")]
        public override int NumOfProposedLots { get; set; }

        public class LlaProposedLotsCheckAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance != null && ((CocAndLlaProjectInfo)validationContext.ObjectInstance).NumOfExisitingLots <= ((int)value))
                    return base.IsValid(value, validationContext);
                return ValidationResult.Success;
            }
        }
    }
}