using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class BuildingHistory : SignatureForm, ICollectionForm
    {
        public override string DisplayName => "Building History";

        [Required]
        [Display(Name = "Statement of Repairs and Improvements")]
        public string StatementOfRepairsAndImprovements { get; set; }

        [Required]
        [Display(Name = "Statement of Building History")]
        public string StatementOfBuildingHistory { get; set; }

        public PermitList PermitList { get; set; }

        public static readonly string PERMIT_KEY = "permitCollId";
        public string[] Keys => new[] {PERMIT_KEY};

        
        public BuildingHistory()
        {
            PermitList = new PermitList();
        }

        public ICollectionAdd GetListCollection(string key)
        {
            return PermitList;
        }

        public object GetEmptyItem(string key)
        {
            return new PermitInfo();
        }
    }
}