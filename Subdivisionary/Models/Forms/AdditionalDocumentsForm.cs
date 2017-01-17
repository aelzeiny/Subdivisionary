using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class AdditionalDocumentsForm : Form
    {
        public AdditionalDocsList AdditionalDocsList { get; set; }
        public override string DisplayName => "Additional Documents Form";

        public AdditionalDocumentsForm()
        {
            AdditionalDocsList = new AdditionalDocsList();
        }
    }

    public class AdditionalDocumentInfo
    {
        public string Title { get; set; }
        public string ScanPath { get; set; }
    }
}