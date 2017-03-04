using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public abstract class SignatureForm: Form, ISignatureForm
    {
        public virtual ICollection<SignatureUploadInfo> Signatures { get; set; }
        public SignatureList SignatureUploadProperties { get; set; }

        public SignatureForm()
        {
            SignatureUploadProperties = new SignatureList();
            Signatures = new List<SignatureUploadInfo>();
        }

        public virtual void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
            var a = after as OwnerForm;
            if (a == null)
                return;

            // If no signature matches owner, then add owner
            foreach (var sig in Signatures)
                if (!(a.Owners.Any(x => x.OwnerName == sig.SignerName)))
                    context.SignatureInfo.Remove(sig);

            // if no properties match the owner, then add property
            foreach (var owner in a.Owners)
                if (SignatureUploadProperties.All(x => x.SignerName != owner.OwnerName))
                {
                    SignatureUploadProperties.Add(new SignatureUploadProperty(owner.OwnerName));
                    this.IsAssigned = false;
                }

            // if no owners match the properties, delete the property
            for (int i = SignatureUploadProperties.Count - 1; i >= 0; i--)
            {
                var mprop = SignatureUploadProperties[i];
                if (a.Owners.All(x => x.OwnerName != mprop.SignerName))
                {
                    SignatureUploadProperties.RemoveAt(i);
                    this.IsAssigned = false;
                }
            }
        }
    }
}