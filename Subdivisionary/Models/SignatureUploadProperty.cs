using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    public struct SignatureUploadProperty
    {
        public string SignerName { get; set; }

        public SignatureUploadProperty(string signerName)
        {
            SignerName = signerName;
        }
    }
}