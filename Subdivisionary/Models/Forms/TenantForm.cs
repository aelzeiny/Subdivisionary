using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class TenantForm : Form, ICollectionForm
    {
        public override string DisplayName => "Property Tenants";

        public TenantsList TenantsList { get; set; }

        public TenantForm()
        {
            TenantsList = new TenantsList();
        }

        public static readonly string TENANT_KEY = "tenantCollId";


        public string[] Keys => new[] {TENANT_KEY};
        public ICollectionAdd GetListCollection(string key)
        {
            return TenantsList;
        }

        public object GetEmptyItem(string key)
        {
            return new TenantInfo();
        }
    }
}