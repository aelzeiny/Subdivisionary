using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.ProjectInfos
{
    public class BasicProjectInfo : IForm
    {
        public int Id { get; set; }


        public int ApplicationId { get; set; }
        public AApplication Application { get; set; }

        [Required]
        public string Block { get; set; }
        [Required]
        public string Lot { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public ContactInfo PrimaryContactInfo { get; set; }

        
        public virtual string DisplayName => "Project Information";
        public virtual string PropertyName => "ProjectInfo";

        [Column("ProjectInfo_IsAssigned")]
        public bool IsAssigned { get; set; }

        public BasicProjectInfo()
        {
            PrimaryContactInfo = new ContactInfo();
            Address = new Address();
        }

        internal void CopyValues(BasicProjectInfo other)
        {
            var type = this.GetType();
            foreach (var props in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (props.CanWrite && props.Name != nameof(Id) && props.Name != nameof(ApplicationId) && props.Name != nameof(Application))
                    props.SetValue(this, props.GetValue(other));
            }
            IsAssigned = true;
        }
    }
}