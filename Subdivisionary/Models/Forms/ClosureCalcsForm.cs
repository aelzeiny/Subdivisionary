using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class ClosureCalcsForm : Form, IUploadableFileForm
    {
        public override string DisplayName => "Electronic Closure Calculations";

        [FileUploadRequired]
        public FileUploadList ClosureCalcsFiles { get; set; }

        [Required]
        public string DanielSampleString { get; set; }

        public ClosureCalcsForm()
        {
            ClosureCalcsFiles = new FileUploadList();
        }

        public FileUploadProperty[] FileUploadProperties()
        {
            return new[]
            {
                new FileUploadProperty(this.Id, "closureCalcsId", "Closure Calcs", "Closure Calculations", false)
            };
        }

        public FileUploadList GetFileUploadList(string key)
        {
            return ClosureCalcsFiles;
        }

        void IUploadableFileForm.SyncFile(string key, FileUploadInfo file)
        {
            //ClosureCalcsFiles.Clear();
            ClosureCalcsFiles.Add(file);
        }
    }
}