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

        void CopyValues(IForm other, bool reverse = false);
    }

    public abstract class Form : IForm
    {
        public int Id { get; set; }
        
        public virtual Application Application { get; set; }
        public int ApplicationId { get; set; }

        public abstract string DisplayName { get; }
        public bool IsAssigned { get; set; }
        public bool IsRequired { get; set; }

        protected Form()
        {
            this.IsAssigned = false;
            this.IsRequired = true;
        }

        public void CopyValues(IForm other, bool reverse)
        {
            var type = this.GetType();
            foreach (var props in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                if (props.CanWrite && (reverse ^ CanCopyProperty(props)))
                    props.SetValue(this, props.GetValue(other));
            IsAssigned = true;
        }

        /// <summary>
        /// This method should return false for items that are not directly manipulated by forms within the view. <br></br>
        /// Basically ask yourself the question, is thisproperty modified directly by a form save? If the answer is no, then this method should return false,
        /// and you have to override the method to make sure that the property isn't copied (either by type or by name).
        /// If the answer is yes, then no need to override the method since the default assumption is that every variable
        /// is directly modifed by a form and should be copied over.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public virtual bool CanCopyProperty(PropertyInfo prop)
        {
            var mType = prop.GetMemberType();
            return prop.Name != nameof(Id) && prop.Name != nameof(ApplicationId) &&
                   prop.Name != nameof(Application) && mType != typeof(ICollection<FileUploadInfo>)
                   && mType != typeof(ICollection<SignatureUploadInfo>) && mType != typeof(SignatureList);
        }
        
        protected virtual void UpdateObserver(Form observer)
        {
        }
    }
}
