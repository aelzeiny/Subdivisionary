using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class NoticeToTenants : Form
    {
        public override string DisplayName => "Notice To All Tenants";

        public static readonly string SAMPLE_NOTICE_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Tenant%20Notice%20of%20Conversion%20Example.pdf";

        [RequiredTrue]
        [Display(Name = "By checking this box the owners of this property affirm, by penalty of perjury, that the tenants have been notified of the pending conversion.")]
        public bool AffirmedNoticeToAllTenants { get; set; }

    }
}