using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class OwnerList : SerializableList<OwnerInfo>
    {
        protected override int ParamCount => 1;

        protected override OwnerInfo ParseObject(string[] param)
        {
            return new OwnerInfo() {OwnerName =  param[0]};
        }

        protected override string[] SerializeObject(OwnerInfo serialize)
        {
            return new[]
            {
                serialize.OwnerName
            };
        }
    }

    public class OwnerInfo
    {
        [Required]
        [Display(Name="Owner Name(s)")]
        public string OwnerName { get; set; }
    }
}