using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    [ComplexType]
    public class PhotographForm : IForm
    {
        public string DisplayName => "Photographs";
        public string PropertyName => "PhotographForm";

        [Column("PhotographForm_ScanPath")]
        public string ScanPath { get; set; }

        [Column("PhotographForm_IsAssigned")]
        public bool IsAssigned { get; set; }

        public PhotographForm()
        {
            IsAssigned = false;
        }
    }
}