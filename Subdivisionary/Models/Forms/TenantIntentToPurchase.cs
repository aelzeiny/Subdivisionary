using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class TenantIntentToPurchase : SignatureForm
    {
        public override string DisplayName => IntentToPurchaseTenantName + " Intent to Purchase";

        [NotMapped]
        public string IntentToPurchaseTenantName
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

        [Required]
        public string IntentToPurchaseTenantUnit { get; set; }

        [Required]
        public string IntentToPurchaseTenantAddress { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal IntentToPurchaseTenantSalePrice { get; set; }

        public override void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
        }

        public override bool CanCopyProperty(PropertyInfo prop)
        {
            return base.CanCopyProperty(prop) && prop.Name != nameof(IntentToPurchaseTenantName);
        }
    }
}