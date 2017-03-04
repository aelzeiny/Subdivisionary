using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class TenantIntentToAcceptOfferOfLtl : SignatureForm
    {
        public override string DisplayName => IntentToAcceptOfferName + " Intent to Accept Offer";

        [Required]
        [Display(Name = "Tenant Address Information")]
        public string IntentToAcceptAddress { get; set; }

        [Required]
        [Display(Name = "Tenant Unit Information")]
        public string IntentToAcceptTenantUnit { get; set; }

        [NotMapped]
        public string IntentToAcceptOfferName
        {
            get
            {
                return SignatureUploadProperties[0].SignerName;
            }
            set
            {
                SignatureUploadProperties.Clear();
                SignatureUploadProperties.Add(new SignatureUploadProperty(value));
            }
        }
    }
}