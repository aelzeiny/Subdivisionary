using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Forms
{
    public class TentativeMapForm : Form, IObservableForm
    {
        public override string DisplayName => IsFinalMap ? "Tentative Final Map" : "Tentative Parcel Map";
        public bool IsFinalMap { get; set; }

        public TentativeMapForm()
        {
            IsFinalMap = false;
        }

        public void UpdateForm(IForm before, IForm after)
        {
            if (!(before is CcBypassInfo))
                return;
            IsFinalMap = ((CcBypassInfo)after).NumberOfUnits > Applications.Application.MAX_PARCEL_MAP_UNITS;
        }
    }
}