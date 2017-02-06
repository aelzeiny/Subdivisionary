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
    public class GrantDeedForm : Form, IUploadableFileForm, ICollectionForm
    {
        public override string DisplayName => "Grant Deeds";

        [FileUploadRequired]
        public FileUploadList GrantPiqFile { get; set; }
        [FileUploadRequired]
        public FileUploadList GrantAdjoinerFiles { get; set; }

        public ApnList ApnList { get; set; }

        private static readonly string GRANT_PIQ_KEY = "grantPIQId";
        private static readonly string GRANT_ADJOINER_KEY = "grantAdjoinerId";
        private static readonly string GRANT_DIRECTORY = "Grant Deed";

        public GrantDeedForm()
        {
            GrantPiqFile = new FileUploadList();
            GrantAdjoinerFiles = new FileUploadList();
            ApnList = new ApnList();
        }

        #region IUploadableFileForm Implementation
        public FileUploadProperty[] FileUploadProperties()
        {
            FileUploadProperty[] property = new FileUploadProperty[2]
            {
                new FileUploadProperty(GRANT_PIQ_KEY, GRANT_DIRECTORY, "Deed PIQ"),
                new FileUploadProperty(GRANT_ADJOINER_KEY, GRANT_DIRECTORY, "Deed Adjoiner", false)
            };
            return property;
        }

        public FileUploadList GetFileUploadList(string key)
        {
            if (key == GRANT_PIQ_KEY)
            {
                return GrantPiqFile;
            }
            else if (key == GRANT_ADJOINER_KEY)
            {
                return GrantAdjoinerFiles;
            }
            return null;
        }

        public void SyncFile(string key, string file)
        {
            if (key == GRANT_PIQ_KEY)
            {
                GrantPiqFile.Clear();
                GrantPiqFile.Add(file);
            }
            else if(key == GRANT_ADJOINER_KEY)
            {
                GrantAdjoinerFiles.Add(file);
            }
        }
        #endregion

        #region IListForm Implementation
        public ICollection GetListCollection()
        {
            return ApnList.ToList();
        }

        public object GetEmptyItem()
        {
            return new ApnInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
            ApnList.AddUntilIndex(index, newValue as ApnInfo, GetEmptyItem() as ApnInfo);
        }
        #endregion
    }
}