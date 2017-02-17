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
    public class ApplicationCheckForm : UploadableFileForm, ICollectionForm
    {
        public override ICollection<FileUploadInfo> FileUploads { get; set; }

        public CheckList ChecksList { get; set; }

        public override string DisplayName => "Application Fees";

        public static readonly string CHECK_DIRECTORY = "App Fees";
        public static readonly string CHECK_KEY = "appFeesId";
        public static readonly string CHECKCOLL_KEY = "checkCollFeesId";

        public string[] Keys => new[] { CHECKCOLL_KEY };


        public ApplicationCheckForm()
        {
            ChecksList = new CheckList();
        }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, CHECK_KEY, CHECK_DIRECTORY, "Check") {IsSingleUpload = false}
        };

        /***** ICollectionForm Implementation *****/
        public ICollectionAdd GetListCollection(string key)
        {
            return ChecksList;
        }

        public object GetEmptyItem(string key)
        {
            return new CheckInfo();
        }
    }
}