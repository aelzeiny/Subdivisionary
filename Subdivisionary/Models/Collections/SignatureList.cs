using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class SignatureList : SerializableList<SignatureUploadProperty>
    {
        protected override int ParamCount => 1;
        protected override SignatureUploadProperty ParseObject(string[] param)
        {
            return new SignatureUploadProperty()
            {
                SignerName = param[0]
            };
        }

        protected override string[] SerializeObject(SignatureUploadProperty serialize)
        {
            return new []
            {
                serialize.SignerName
            };
        }
    }
}