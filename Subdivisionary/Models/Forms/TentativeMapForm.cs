using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Forms
{
    public class TentativeMapForm : UploadableFileForm, IObserverForm
    {
        public override string DisplayName => IsFinalMap ? "Tentative Final Map" : "Tentative Parcel Map";
        
        [Display(Name = "This box will be automatically checked if this application is a final map")]
        public bool IsFinalMap { get; set; }

        [Display(Name = "Check this box if this is a Vesting Tentative Map")]
        public bool IsVestingMap { get; set; }

        public static readonly string TENTATIVE_MAP_KEY = "tentativeMapId";
        public static readonly string TENTATIVE_MAP_DIRECTORY = "Tentative Map";

        public TentativeMapForm()
        {
            IsFinalMap = false;
        }

        public void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
            if (!(before is CcBypassInfo))
                return;
            IsFinalMap = ((CcBypassInfo)after).IsFinalMap();
        }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, TENTATIVE_MAP_KEY, TENTATIVE_MAP_DIRECTORY, "Tentative Map", true)
        };
        
        public override bool CanCopyProperty(PropertyInfo prop)
        {
            // IsFinalMap is a property that is not determined by submitting forms, and thus we shouldn't copy its value
            return base.CanCopyProperty(prop) && prop.Name != nameof(IsFinalMap);
        }
    }
}