using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public interface ICollectionForm
    {
        string[] Keys { get; }
        ICollectionAdd GetListCollection(string key);
        object GetEmptyItem(string key);
    }
}
