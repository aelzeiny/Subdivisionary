using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subdivisionary.Models.Collections
{

    /*public float Amount { get; set; }
    public string ScanPath { get; set; }
    public string CheckNumber { get; set; }
    public string RoutingNumber { get; set; }
    public string AccountNumber { get; set; }*/
    [ComplexType]
    public class CheckList : CsvList<CheckInfo>
    {
        protected override int ParamCount
        {
            get { return 5; }
        }

        protected override CheckInfo ParseObject(string[] param)
        {
            return new CheckInfo()
            {
                Amount = float.Parse(param[0]),
                ScanPath = param[1],
                CheckNumber = param[2],
                RoutingNumber = param[3],
                AccountNumber = param[4]
            };
        }

        protected override string[] SerializeObject(CheckInfo serialize)
        {
            return new string[]
            {
                serialize.Amount.ToString(),
                serialize.ScanPath,
                serialize.CheckNumber,
                serialize.RoutingNumber,
                serialize.AccountNumber
            };
        }
    }
}