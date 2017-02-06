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
    public class SignatureList : SerializableList<SignatureInfo>
    {
        protected override int ParamCount => 4;
        protected override SignatureInfo ParseObject(string[] param)
        {
            return new SignatureInfo()
            {
                SignerName = param[0],
                SignDate = DateTime.Parse(param[1]),
                Formatting = param[2],
                Data = param[3]
            };
        }

        protected override string[] SerializeObject(SignatureInfo serialize)
        {
            return new string[]
            {
                serialize.SignerName,
                serialize.SignDate.ToString(),
                serialize.Formatting,
                serialize.Data
            };
        }
    }

    public class SignatureInfo
    {
        public string SignerName { get; set; }
        public DateTime SignDate { get; set; }
        public string Formatting { get; set; }
        public string Data { get; set; }
    }
}