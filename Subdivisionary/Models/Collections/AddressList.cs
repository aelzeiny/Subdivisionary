using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class AddressList : SerializableList<ParcelInfo>
    {
        protected override int ParamCount => 5;
        protected override ParcelInfo ParseObject(string[] param)
        {
            return new ParcelInfo()
            {
                Block = param[0],
                Lot = param[1],
                AddressRangeStart = param[2],
                AddressRangeEnd = param[3],
                AddressStreet = param[4]
            };
        }

        protected override string[] SerializeObject(ParcelInfo serialize)
        {
            return new[]
            {
                serialize.Block,
                serialize.Lot,
                serialize.AddressRangeStart,
                serialize.AddressRangeEnd,
                serialize.AddressStreet
            };
        }

        public override string ToString()
        {
            string answer = "";
            foreach (var select in data)
                answer += select.ToString() + ", ";
            return answer.Substring(0, Math.Max(0, answer.Length - 2));
        }
    }

    public class ParcelInfo
    {
        [Required]
        public string Block { get; set; }
        [Required]
        public string Lot { get; set; }
        [Required]
        [DisplayName("Range Start")]
        public string AddressRangeStart { get; set; }
        [Required]
        [DisplayName("Range End")]
        public string AddressRangeEnd { get; set; }
        [Required]
        [DisplayName("Street")]
        public string AddressStreet { get; set; }

        public override string ToString()
        {
            return string.Format("({0}/{1}) {2}-{3} {4}", Block, Lot, AddressRangeStart, AddressRangeEnd, AddressStreet);
        }
    }
}