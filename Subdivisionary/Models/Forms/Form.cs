using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.Internal;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public interface IForm
    {
        string DisplayName { get; }
        bool IsAssigned { get; set; }


        int Id { get; set; }


        int ApplicationId { get; set; }
        Application Application { get; set; }
        bool IsRequired { get; }

        void CopyValues(IForm other);
    }

    public abstract class Form : IForm
    {
        public int Id { get; set; }
        
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        public abstract string DisplayName { get; }
        public bool IsAssigned { get; set; }
        public bool IsRequired { get; set; }

        protected Form()
        {
            this.IsAssigned = false;
            this.IsRequired = true;
        }

        public void CopyValues(IForm other)
        {
            var type = this.GetType();
            foreach (var props in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                if (CanCopyProperty(props))
                    props.SetValue(this, props.GetValue(other));
            IsAssigned = true;
        }

        public virtual bool CanCopyProperty(PropertyInfo prop)
        {
            return prop.CanWrite && prop.Name != nameof(Id) && prop.Name != nameof(ApplicationId) &&
                   prop.Name != nameof(Application) && prop.GetMemberType() != (typeof(FileUploadList));
        }
        
        protected virtual void UpdateObserver(Form observer)
        {
        }
    }
}
