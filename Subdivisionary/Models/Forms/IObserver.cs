using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public interface IObserverForm
    {
        void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after);
    }
}