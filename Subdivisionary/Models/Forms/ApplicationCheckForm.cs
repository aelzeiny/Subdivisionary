using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class ApplicationCheckForm : Form, IUploadableFileForm, ICollectionForm
    {
        [FileUploadRequired]
        public FileUploadList ChecksUploadList { get; set; }

        public CheckList ChecksList { get; set; }

        public override string DisplayName => "Application Fees";

        public static readonly string CHECK_DIRECTORY = "App Fees";
        public static readonly string CHECK_KEY = "appFeesId";


        public ApplicationCheckForm()
        {
            ChecksUploadList = new FileUploadList();
            ChecksList = new CheckList();
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            FileUploadProperty[] property = 
            {
                new FileUploadProperty(this.Id, CHECK_KEY, CHECK_DIRECTORY, "Check") { IsSingleUpload = false}
            };
            return property;
        }

        public FileUploadList GetFileUploadList(string key)
        {
            return (key == CHECK_KEY) ? ChecksUploadList : null;
        }

        public void SyncFile(string key, FileUploadInfo file)
        {
            this.ChecksUploadList.Add(file);
        }

        public ICollection GetListCollection()
        {
            return ChecksList.ToList();
        }

        public object GetEmptyItem()
        {
            return new CheckInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
            ChecksList.AddUntilIndex(index, newValue as CheckInfo, GetEmptyItem() as CheckInfo);
        }
    }
}