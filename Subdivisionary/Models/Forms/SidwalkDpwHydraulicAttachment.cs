using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkDpwHydraulicAttachment : UploadableFileForm, ICollectionForm
    {
        public override string DisplayName => "DPW Hydraulic Attachment";

        public static readonly string HYDRAULIC_UPLOAD_KEY = "hydraulicEngrUploadId";
        public static readonly string HYDRAULIC_COLL_KEY = "hydraulicEngrCollId";

        public DateNameList HydraulicDateNameList { get; set; }


        [Required]
        [Display(Name = "1. Has your project been reviewed by the Hydraulic Engineering Section?")]
        public bool SidewalkProposalReviewedByHydraulicEngrs { get; set; }

        [Required]
        [Display(Name ="2. Proposal Meets Hundred Year Requirements")]
        public bool SidewalkProposalMeetsHundredYrRequirements { get; set; }

        public SidewalkDpwHydraulicAttachment()
        {
            HydraulicDateNameList = new DateNameList();
        }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, HYDRAULIC_UPLOAD_KEY, "Attachments", "Hydraulic Engr", isRequiredUpload:SidewalkProposalMeetsHundredYrRequirements),
        };

        public string[] Keys => new[] {HYDRAULIC_COLL_KEY};

        public ICollectionAdd GetListCollection(string key)
        {
            return HydraulicDateNameList;
        }

        public object GetEmptyItem(string key)
        {
            return new DateNameInfo();
        }
    }
}