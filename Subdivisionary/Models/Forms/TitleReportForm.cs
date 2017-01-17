using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class TitleReportForm : Form
    {
        public TitleCompany Company { get; set; } = TitleCompany.Other;

        public string ScanPath { get; set; }
        public override string DisplayName => "Preliminary Title Report";

        public TitleReportForm()
        {
        }
    }
    
    public enum TitleCompany
    {
        Chicago, FirstAmerican, Fidelity, OldRepublic, Other
    }
}