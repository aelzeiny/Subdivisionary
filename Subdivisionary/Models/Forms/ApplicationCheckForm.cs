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
    [ComplexType]
    public class ApplicationCheckForm : IForm
    {
        public CheckList Checks { get; set; }
        public string DisplayName => "Application Fees";
        public string PropertyName => "ApplicationCheckForm";

        [Column("ApplicationCheckForm_IsAssigned")]
        public bool IsAssigned { get; set; }

        public ApplicationCheckForm()
        {
            Checks = new CheckList();
            IsAssigned = false;
        }
    }

    //[ComplexType]
    public class CheckInfo
    {
        public float Amount { get; set; }
        public string ScanPath { get; set; }

        [DisplayName("Check Number")]
        public string CheckNumber { get; set; }
        [DisplayName("Routing Number")]
        public string RoutingNumber { get; set; }
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
    }
}