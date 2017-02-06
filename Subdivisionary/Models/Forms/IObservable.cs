using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public interface IObservableForm
    {
        void UpdateForm(IForm before, IForm after);
    }
}