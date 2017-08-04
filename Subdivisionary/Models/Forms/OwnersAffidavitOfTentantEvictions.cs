using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class OwnersAffidavitOfTentantEvictions: UploadableFileForm
    {
        public override string DisplayName => "Owner Affidavit of Evictions";

        public static readonly string AFFIDAVIT_TENANT_EV_KEY = "affidavitTenantEvId";
        public static readonly string AFFIDAVIT_PROTECTED_EV_KEY = "affidavitProtectedEvId";
        public static readonly string AFFIDAVIT_EV_DIRECTORY = "Owners Affidavit of Tenant Evictions";

        public static readonly string SAMPLE_TENANT_EV_URL = "~/Samples/Affidavit Protected Tenants Evictions Template.pdf";
        public static readonly string SAMPLE_PROTECTED_EV_URL = "~/Samples/Affidavit Tenant Evictions Template.pdf";
        
        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, AFFIDAVIT_TENANT_EV_KEY, AFFIDAVIT_EV_DIRECTORY, "Affidavit Tenant Evictions"), 
            new FileUploadProperty(this.Id, AFFIDAVIT_PROTECTED_EV_KEY, AFFIDAVIT_EV_DIRECTORY, "Affidavit Protected Class Tenant Evictions") 
        };
    }
}