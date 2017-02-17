using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class OwnersAffidavitOfTentantEvictions: Form, ISignatureForm
    {
        public override string DisplayName => "Owner Affidavit of Tenant Evictions";
        
        public virtual ICollection<SignatureUploadInfo> Signatures { get; set; }
        public SignatureList SignatureUploadProperties { get; set; }

        public OwnersAffidavitOfTentantEvictions()
        {
            SignatureUploadProperties = new SignatureList();
            Signatures = new List<SignatureUploadInfo>();
        }

        public void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
            var a = after as OwnerForm;
            if (a == null)
                return;
            foreach (var sig in Signatures)
                if (!(a.Owners.Any(x => x.OwnerName == sig.SignerName)))
                    context.SignatureInfo.Remove(sig);
            SignatureUploadProperties.Clear();
            SignatureUploadProperties.AddRange(a.Owners.Select(x=>new SignatureUploadProperty(x.OwnerName)));
        }
    }
}