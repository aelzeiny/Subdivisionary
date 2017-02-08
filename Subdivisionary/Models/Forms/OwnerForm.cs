using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public class OwnerForm : Form, ICollectionForm
    {
        public override string DisplayName => "Legal Owners";

        public OwnerList Owners { get; set; }

        public OwnerForm()
        {
            Owners = new OwnerList();
        }
        
        public ICollection GetListCollection()
        {
            return Owners.ToList();
        }

        public object GetEmptyItem()
        {
            return new OwnerInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
            Owners.AddUntilIndex(index, (OwnerInfo) newValue, new OwnerInfo());
        }
    }
}