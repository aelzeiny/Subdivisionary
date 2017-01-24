using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class TitleReportForm : Form, IUploadableFileForm, ICollectionForm
    {
        public TitleCompany TitleCompany { get; set; }
        
        public override string DisplayName => "Preliminary Title Report";

        public FileUploadList PtrFile { get; set; }
        public PtrContactList PtrContactList { get; set; }

        [DisplayName("Order/Escrow Number")]
        public string OrderNumber { get; set; }
        [DisplayName("Other Title Company (if applicable)")]
        public string OtherTitleCompany { get; set; }

        private static readonly string PTR_KEY = "ptrId";
        private static readonly string PTR_DIRECTORY = "PTR";

        public TitleReportForm()
        {
            PtrFile = new FileUploadList();
            PtrContactList = new PtrContactList();
            //TitleCompany = TitleCompany.Other;
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            return new FileUploadProperty[]
            {
                new FileUploadProperty(PTR_KEY, PTR_DIRECTORY, "PTR"), 
            };
        }

        public FileUploadList GetFileUploadList(string key)
        {
            return PtrFile;
        }

        public void SyncFile(string key, string file)
        {
            PtrFile.Clear();
            PtrFile.Add(file);
        }

        public ICollection GetListCollection()
        {
            return PtrContactList.ToList();
        }

        public object GetEmptyItem()
        {
            return new PtrContactInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
            PtrContactList.AddUntilIndex(index, (PtrContactInfo)newValue, (PtrContactInfo)GetEmptyItem());
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