using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    public class PermitList : SerializableList<PermitInfo>
    {
        protected override int ParamCount => 1;
        protected override PermitInfo ParseObject(string[] param)
        {
            return new PermitInfo() {PermitId = int.Parse(param[0])};
        }

        protected override string[] SerializeObject(PermitInfo serialize)
        {
            return new[] {serialize.PermitId.ToString()};
        }
    }

    public class PermitInfo
    {
        [Range(1, int.MinValue)]
        public int PermitId { get; set; }
    }
}