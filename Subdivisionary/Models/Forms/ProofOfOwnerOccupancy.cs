using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class ProofOfOwnerOccupancy : UploadableFileForm
    {
        public override string DisplayName => "Proof Of Occupancy";

        public static readonly string AFFIDAVIT_KEY = "affidavitId";
        public static readonly string HO_EXEMPTION_KEY = "hoExemptionId";

        public static readonly string OWNER_OCCUPANCY_DIRECTORY = "Proof Of Occupancy";
        public static readonly string AFFIDAVIT_URL = "https://subdivisionaryblob.blob.core.windows.net/templates/Affidavit%20Ownership%20and%20Occupancy%20Template.pdf";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, AFFIDAVIT_KEY, OWNER_OCCUPANCY_DIRECTORY, "Affidavit for Ownership or Occupancy", false),
            new FileUploadProperty(this.Id, HO_EXEMPTION_KEY, OWNER_OCCUPANCY_DIRECTORY, "Homeowner Exemption Form", false)
        };

    }
}