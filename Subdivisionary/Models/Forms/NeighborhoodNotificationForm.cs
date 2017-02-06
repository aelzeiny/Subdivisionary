using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Forms
{
    public class NeighborhoodNotificationForm : Form, IObservableForm
    {
        public override string DisplayName => "";//Is300FootNotice ? "Neighboorhood Notice" : "Local Notice";

        /*public bool Is300FootNotice { get; set; }

        public NeighborhoodNotificationForm()
        {
            Is300FootNotice = false;
        }*/

        public void UpdateForm(IForm before, IForm after)
        {
            if (before is CcBypassInfo && this.Application is CcBypass)
            {
                var b = (CcBypassInfo) before;
                var a = (CcBypassInfo) after;
            }
        }
    }
}