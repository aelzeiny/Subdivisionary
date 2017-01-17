using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class ClosureCalcsForm : Form
    {
        public string ScanPath { get; set; }
        public override string DisplayName => "Electronic Closure Calculations";

        public ClosureCalcsForm()
        {
        }
    }
}