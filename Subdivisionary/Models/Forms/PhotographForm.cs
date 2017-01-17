using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class PhotographForm : Form
    {
        public override string DisplayName => "Photographs";
        
        public string ScanPath { get; set; }
        public PhotographForm()
        {
        }
    }
}