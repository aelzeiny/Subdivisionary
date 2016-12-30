using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subdivisionary.Models.Collections
{
    /*
    {
        public string Block { get; set; }
        public string Lot { get; set; }

        public string ScanPath { get; set; }
    }
    */
    [ComplexType]
    public class GrantDeedList : CsvList<GrantDeedInfo>
    {
        protected override int ParamCount => 3;

        protected override GrantDeedInfo ParseObject(string[] param)
        {
            return new GrantDeedInfo()
            {
                Block = param[0],
                Lot = param[1],
                ScanPath = param[2]
            };
        }

        protected override string[] SerializeObject(GrantDeedInfo serialize)
        {
            return new string[]
            {
                serialize.Block,
                serialize.Lot,
                serialize.ScanPath
            };
        }
    }
}