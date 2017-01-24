using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class ApnList : SerializableList<ApnInfo>
    {
        protected override int ParamCount => 2;

        protected override ApnInfo ParseObject(string[] param)
        {
            return new ApnInfo()
            {
                Block = param[0],
                Lot = param[1]
            };
        }

        protected override string[] SerializeObject(ApnInfo serialize)
        {
            return new string[]
            {
                serialize.Block,
                serialize.Lot
            };
        }
    }

    public class ApnInfo
    {
        public string Block { get; set; }
        public string Lot { get; set; }
    }
}