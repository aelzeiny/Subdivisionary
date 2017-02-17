using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.DAL;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class GrantDeedForm : UploadableFileForm, ICollectionForm
    {
        public override string DisplayName => "Grant Deeds";
        
        public override ICollection<FileUploadInfo> FileUploads { get; set; }

        public ApnList ApnList { get; set; }

        public static readonly string GRANT_PIQ_KEY = "grantPIQId";
        public static readonly string GRANT_ADJOINER_KEY = "grantAdjoinerId";
        public static readonly string GRANT_DIRECTORY = "Grant Deed";

        public static readonly string APN_KEY = "apnInfoId";
        public string[] Keys => new[] { APN_KEY };

        public GrantDeedForm()
        {
            ApnList = new ApnList();
        }
        
        public override FileUploadProperty[] FileUploadProperties => new []
        {
            new FileUploadProperty(this.Id, GRANT_PIQ_KEY, GRANT_DIRECTORY, "Deed PIQ", false),
            new FileUploadProperty(this.Id, GRANT_ADJOINER_KEY, GRANT_DIRECTORY, "Deed Adjoiner", false)
        };

        public ICollectionAdd GetListCollection(string key)
        {
            return ApnList;
        }

        public object GetEmptyItem(string key)
        {
            return new ApnInfo();
        }
    }
}