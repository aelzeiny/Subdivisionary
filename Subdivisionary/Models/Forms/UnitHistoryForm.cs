using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class UnitHistoryForm : Form, IObserverForm
    {
        public override string DisplayName => (UnitHistoryNumber == null) ? "Unit History" : $"Unit #{UnitHistoryNumber} History ";

        public string UnitHistoryNumber { get; set; }

        public void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
            throw new NotImplementedException();
        }
    }
}