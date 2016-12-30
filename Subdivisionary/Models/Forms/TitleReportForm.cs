using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class TitleReportForm : IForm
    {
        [DisplayName("TitleReportForm_TitleCompany")]
        public TitleCompany Company { get; set; } = TitleCompany.Other;
        [Column("TitleReportForm_ScanPath")]
        public string ScanPath { get; set; }
        public string DisplayName => "Preliminary Title Report";
        public string PropertyName => "TitleReportForm";

        [Column("TitleReportForm_IsAssigned")]
        public bool IsAssigned { get; set; }
        public TitleReportForm()
        {
            IsAssigned = false;
        }
    }
    
    public enum TitleCompany
    {
        Chicago, FirstAmerican, Fidelity, OldRepublic, Other
    }
}