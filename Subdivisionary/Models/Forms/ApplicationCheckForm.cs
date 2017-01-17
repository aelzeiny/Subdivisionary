using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class ApplicationCheckForm : Form, IUploadableFileForm
    {
        public CheckList Checks { get; set; }

        public override string DisplayName => "Application Fees";

        public static readonly string CHECK_DIRECTORY = "App Fees";
        public static readonly string CHECK_KEY = "appFeesId";


        public ApplicationCheckForm()
        {
            Checks = new CheckList();
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            FileUploadProperty[] property = 
            {
                new FileUploadProperty(CHECK_KEY, CHECK_DIRECTORY, "Check") { IsSingleUpload = false}
            };
            return property;
        }

        public FileUploadList[] FileUploadsLists()
        {
            return new FileUploadList[]
            {
                Checks
            };
        }

        public FileUploadList GetFileUploadList(string key)
        {
            return Checks;
        }

        public void SyncFile(string key, string file)
        {
            this.Checks.Add(new CheckInfo() {FilePath = file});
        }
    }
}