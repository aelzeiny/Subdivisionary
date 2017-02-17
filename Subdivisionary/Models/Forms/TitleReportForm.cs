using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class TitleReportForm : UploadableFileForm, ICollectionForm
    {
        public TitleCompany TitleCompany { get; set; }
        
        public override string DisplayName => "Preliminary Title Report";
        
        public PtrContactList PtrContactList { get; set; }

        [Required]
        [DisplayName("Order/Escrow Number")]
        public string OrderNumber { get; set; }

        [TitleCompanyOtherValidation]
        [DisplayName("Other Title Company (if applicable)")]
        public string OtherTitleCompany { get; set; }

        public static readonly string PTR_KEY = "ptrId";
        public static readonly string PTR_DIRECTORY = "PTR";

        public static readonly string CONTACT_KEY = "ptrCollectionId";
        public string[] Keys => new[] { CONTACT_KEY };

        public TitleReportForm()
        {
            PtrContactList = new PtrContactList();
        }

        public override FileUploadProperty[] FileUploadProperties => new []
        {
            new FileUploadProperty(this.Id, PTR_KEY, PTR_DIRECTORY, "PTR"), 
        };

        public ICollectionAdd GetListCollection(string key)
        {
            return PtrContactList;
        }

        public object GetEmptyItem(string key)
        {
            return new PtrContactInfo();
        }
    }
    
    public enum TitleCompany
    {
        [Display(Name = "Chicago")]
        Chicago,
        [Display(Name = "First American")]
        FirstAmerican,
        [Display(Name = "Fidelity")]
        Fidelity,
        [Display(Name = "Old Republic")]
        OldRepublic,
        [Display(Name = "Other")]
        Other
    }
}