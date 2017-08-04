using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class SidewalkBsmAttachment : Form, ICollectionForm
    {
        public override string DisplayName => "BSM Attachment";

        public static readonly string BSM_PERMITS_COLL_KEY = "bsmPermitsId";

        [RequiredTrue]
        [Display(Name = "Have you submitted an application for a Street-Improvement Permit?")]
        public bool HasSubmittedStreetInprovementPermit { get; set; }
        public PermitNameList BsmPermitNameList { get; set; }

        public string[] Keys => new[] {BSM_PERMITS_COLL_KEY};

        public SidewalkBsmAttachment()
        {
            BsmPermitNameList = new PermitNameList();
        }

        public ICollectionAdd GetListCollection(string key)
        {
            return BsmPermitNameList;
        }

        public object GetEmptyItem(string key)
        {
            return new PermitNameInfo();
        }

    }
}