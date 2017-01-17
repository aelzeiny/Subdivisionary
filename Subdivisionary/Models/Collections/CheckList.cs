using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class CheckList : FileUploadList<CheckInfo>
    {
        protected override int ParamCount => 5;

        protected override IFileUploadItem ParseObject(string[] param)
        {
            return new CheckInfo()
            {
                Amount = float.Parse(param[0]),
                FilePath = param[1],
                CheckNumber = param[2],
                RoutingNumber = param[3],
                AccountNumber = param[4]
            };
        }

        protected override string[] SerializeObject(IFileUploadItem serial)
        {
            CheckInfo serialize = (CheckInfo) serial;
            return new string[]
            {
                serialize.Amount.ToString(),
                serialize.FilePath,
                serialize.CheckNumber,
                serialize.RoutingNumber,
                serialize.AccountNumber
            };
        }
    }
    
    public class CheckInfo : IFileUploadItem
    {
        public float Amount { get; set; }

        [DisplayName("Check Number")]
        public string CheckNumber { get; set; }
        [DisplayName("Routing Number")]
        public string RoutingNumber { get; set; }
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        public string FilePath { get; set; }
    }
}