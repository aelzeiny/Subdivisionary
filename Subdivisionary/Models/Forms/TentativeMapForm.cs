using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class TentativeMapForm : Form
    {
        public override string DisplayName => IsFinalMap ? "Tentative Final Map" : "Tentative Parcel Map";
        public bool IsFinalMap { get; set; }

        public TentativeMapForm() : base()
        {
            IsFinalMap = false;
        }
    }
}