using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public interface ISignatureForm : IObserverForm
    {
        int Id { get; set; }
        ICollection<SignatureUploadInfo> Signatures { get; set; }
        SignatureList SignatureUploadProperties { get; set; }
    }
}
