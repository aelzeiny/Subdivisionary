using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    [ComplexType]
    public class AdditionalDocumentsForm : IForm
    {
        public AdditionalDocsList AdditionalDocsList { get; set; }
        public string DisplayName => "Additional Forms";
        public string PropertyName => "AdditionalDocumentsForm";

        [Column("AdditionalDocumentsForm_IsAssigned")]
        public bool IsAssigned { get; set; }

        public AdditionalDocumentsForm()
        {
            AdditionalDocsList = new AdditionalDocsList();
            IsAssigned = false;
        }
    }

    public class AdditionalDocumentInfo
    {
        public string Title { get; set; }
        public string ScanPath { get; set; }
    }
}