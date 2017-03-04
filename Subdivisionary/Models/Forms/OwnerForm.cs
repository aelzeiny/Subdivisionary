using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Subdivisionary.Models.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subdivisionary.Models.Forms
{
    public class OwnerForm : Form, ICollectionForm
    {
        public override string DisplayName => "Property Owners";

        public string[] Keys => new[] { OWNERS_KEY };
        public static readonly string OWNERS_KEY = "ownersId";
        
        public OwnerList Owners { get; set; }

        public OwnerForm()
        {
            Owners = new OwnerList();
        }
        
        public ICollectionAdd GetListCollection(string key)
        {
            return Owners;
        }

        public object GetEmptyItem(string key)
        {
            return new OwnerInfo();
        }
    }
}