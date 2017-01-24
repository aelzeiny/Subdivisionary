using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class CheckList : SerializableList<CheckInfo>
    {
        protected override int ParamCount => 4;

        protected override CheckInfo ParseObject(string[] param)
        {
            return new CheckInfo()
            {
                Amount = float.Parse(param[0]),
                CheckNumber = param[1],
                RoutingNumber = param[2],
                AccountNumber = param[3]
            };
        }

        protected override string[] SerializeObject(CheckInfo serial)
        {
            CheckInfo serialize = (CheckInfo) serial;
            return new string[]
            {
                serialize.Amount.ToString(),
                serialize.CheckNumber,
                serialize.RoutingNumber,
                serialize.AccountNumber
            };
        }
    }
    
    public class CheckInfo
    {
        [Required]
        public float Amount { get; set; }
        [DisplayName("Check Number")]
        [Required]
        public string CheckNumber { get; set; }
        [DisplayName("Routing Number")]
        [Required]
        public string RoutingNumber { get; set; }
        [DisplayName("Account Number")]
        [Required]
        public string AccountNumber { get; set; }
    }
}