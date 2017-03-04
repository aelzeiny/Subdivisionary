using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class SummaryOfTenantContacts : SignatureForm, ICollectionForm
    {
        public override string DisplayName => "Summary Of Tenant Contacts";

        public static readonly string TENANT_CONTACTS_KEY = "tenantContactsId";

        public string[] Keys => new[] {TENANT_CONTACTS_KEY};
        
        public TenantContactsList TenantContactsList { get; set; }

        public SummaryOfTenantContacts()
        {
            TenantContactsList = new TenantContactsList();
        }

        public ICollectionAdd GetListCollection(string key)
        {
            return TenantContactsList;
        }

        public object GetEmptyItem(string key)
        {
            return new TenantContactInfo();
        }
    }
}