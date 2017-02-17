using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class TenantList : SerializableList<TenantInfo>
    {
        protected override int ParamCount => 1;

        protected override TenantInfo ParseObject(string[] param)
        {
            return new TenantInfo() { TenantName = param[0] };
        }

        protected override string[] SerializeObject(TenantInfo serialize)
        {
            return new[]
            {
                serialize.TenantName
            };
        }
    }

    public class TenantInfo
    {
        [Required]
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

         
    }
}