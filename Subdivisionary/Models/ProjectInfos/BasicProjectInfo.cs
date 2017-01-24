using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Ajax.Utilities;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.ProjectInfos
{
    public class BasicProjectInfo : IForm, ICollectionForm
    {
        public int Id { get; set; }


        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public bool IsAssigned { get; set; }
        public bool IsRequired => true;

        public AddressList AddressList { get; set; }
        
        [Required]
        public ContactInfo PrimaryContactInfo { get; set; }
        
        public virtual string DisplayName => "Project Information";

        public BasicProjectInfo()
        {
            PrimaryContactInfo = new ContactInfo();
            AddressList = new AddressList();
        }

        public void CopyValues(IForm other)
        {
            var type = this.GetType();
            foreach (var props in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (props.CanWrite && props.Name != nameof(Id) && props.Name != nameof(ApplicationId) && props.Name != nameof(Application))
                    props.SetValue(this, props.GetValue(other));
            }
            IsAssigned = true;
        }

        public ICollection GetListCollection()
        {
            return AddressList.ToList();
        }

        public object GetEmptyItem()
        {
            return new ParcelInfo();
        }

        public void ModifyCollection(int index, object newValue)
        {
            AddressList.AddUntilIndex(index, (ParcelInfo)newValue, (ParcelInfo)GetEmptyItem());
        }

        public override string ToString()
        {
            return string.Join(", ",
                this.AddressList.Select(
                    x =>
                        "(" + x.Block + "/" + x.Lot + ") " + x.AddressRangeStart + " - " + x.AddressRangeEnd + " " +
                        x.AddressStreet));
        }
    }
}