using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class CcEcp : Application
    {
        protected override void Init()
        {
            ProjectInfo = new CcEcpInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
            };
        }

        public override void FormUpdated(ApplicationDbContext context, IForm before, IForm after)
        {
            if (after is CcEcpInfo)
            {
                var a = (CcEcpInfo) after;
                var b = (CcEcpInfo) before;
                // This is a special case-scenario for ECP Projects.
                // When the number of units is added or reduced we add or reduce the number of UnitHistory Forms
                var delta = a.NumberOfUnits - b.NumberOfUnits;
                // positive delta means too many forms

                if (delta > 0)
                {
                    for (int i = 1; i <= delta; i++)
                        this.Forms.Add(new UnitHistoryForm());
                }
                else if (delta < 0)
                {
                    var unitForms = this.Forms.Where(x => x is UnitHistoryForm).ToArray();
                    for (int i = b.NumberOfUnits; i >= a.NumberOfUnits; i--)
                        this.Forms.Remove(unitForms[unitForms.Length - (b.NumberOfUnits - i)]);

                }
            }
            base.FormUpdated(context, before, after);
        }

        public override string DisplayName => "Expedited Conversion Program";
    }
}