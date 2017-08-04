using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class MarketRateList : SerializableList<MarketRateInfo>
    {
    }

    public class MarketRateInfo
    {
        public string ApartmentNo { get; set; }
        public float ProposedSalesPrice { get; set; }
    }
}