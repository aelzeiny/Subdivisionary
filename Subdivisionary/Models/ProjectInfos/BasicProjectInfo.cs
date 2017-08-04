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
    /// <summary>
    /// Base class for all Project Info Types. Specifically for Record of Surveys, which
    /// have the least basic requirements in comparison to all apps.
    /// </summary>
    public class BasicProjectInfo : IForm, ICollectionForm
    {
        public static readonly string APN_INFO_KEY = "apnInfoId";

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsRequired => true;
        public AddressList AddressList { get; set; }
        
        [Required]
        public ContactInfo PrimaryContactInfo { get; set; }

        public ContactInfo OwnerContactInfo { get; set; }

        public bool OwnerAndPrimaryContactAreSame { get; set; }

        public virtual string DisplayName => "Project Information";

        public string[] Keys => new[] { APN_INFO_KEY };

        public BasicProjectInfo()
        {
            PrimaryContactInfo = new ContactInfo();
            OwnerContactInfo = new ContactInfo();
            AddressList = new AddressList();
            OwnerAndPrimaryContactAreSame = false;
        }

        public void CopyValues(IForm other, bool reverse)
        {
            var type = this.GetType();
            foreach (var props in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (props.CanWrite && (reverse ^ (props.Name != nameof(Id) && props.Name != nameof(ApplicationId) && props.Name != nameof(Application))))
                    props.SetValue(this, props.GetValue(other));
            }
            IsAssigned = true;
        }

        public ICollectionAdd GetListCollection(string name)
        {
            return AddressList;
        }

        public object GetEmptyItem(string name)
        {
            return new ParcelInfo();
        }


        public string Addresses()
        {
            return string.Join(",",
                AddressList.Select(x => x.AddressRangeStart + " - " + x.AddressRangeEnd + " " + x.AddressStreet));
        }
        public string Apns()
        {
            return string.Join(",", this.AddressList.Select(x => x.Block + "/" + x.Lot));
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