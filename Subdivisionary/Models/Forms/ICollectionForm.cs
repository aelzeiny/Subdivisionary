using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subdivisionary.Models.Forms
{
    public interface ICollectionForm
    {
        ICollection GetListCollection();
        object GetEmptyItem();
        void ModifyCollection(int index, object newValue);
    }
}
