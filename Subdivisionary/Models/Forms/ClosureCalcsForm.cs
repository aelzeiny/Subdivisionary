using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    [ComplexType]
    public class ClosureCalcsForm : IForm
    {
        [Column("ClosureCalcsForm_ScanPath")]
        public string ScanPath { get; set; }
        public string DisplayName => "Electronic Closure Calculations";
        public string PropertyName => "ClosureCalcsForm";

        [Column("ClosureCalcsForm_IsAssigned")]
        public bool IsAssigned { get; set; }

        public ClosureCalcsForm()
        {
            IsAssigned = false;
        }
    }
}