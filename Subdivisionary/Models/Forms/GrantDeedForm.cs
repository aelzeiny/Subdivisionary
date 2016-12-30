using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class GrantDeedForm : IForm
    {
        public GrantDeedList List { get; set; }
        public string DisplayName => "Grant Deeds";
        public string PropertyName => "GrantDeedForm";

        [Column("GrantDeedForm_IsAssigned")]
        public bool IsAssigned { get; set; }
        
        public GrantDeedForm()
        {
            List = new GrantDeedList();
            IsAssigned = false;
        }

    }

    //[ComplexType]
    public class GrantDeedInfo
    {
        public string Block { get; set; }
        public string Lot { get; set; }

        public string ScanPath { get; set; }
    }
}