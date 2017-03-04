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
    }

    public class ApnInfo
    {
        public string Block { get; set; }
        public string Lot { get; set; }
    }
}