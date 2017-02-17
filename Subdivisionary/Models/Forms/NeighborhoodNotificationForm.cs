using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Forms
{
    public class NeighborhoodNotificationForm : UploadableFileForm, IObserverForm
    {
        public override string DisplayName => Is300FootNotice ? "Neighboorhood Notification" : "Local Notification";

        public bool Is300FootNotice { get; set; }

        public static readonly string NEIGHBORHOOD_NOTICE_LIST_KEY = "Neighboorhod Notice";
        public static readonly string NEIGHBORHOOD_NOTICE_300_FOOT_RADIUS_KEY = "nn300FootRadiusId";
        public static readonly string NEIGHBORHOOD_NOTICE_DIRECTORY = "Closure Calcs";

        public NeighborhoodNotificationForm()
        {
            Is300FootNotice = false;
        }

        public void ObserveFormUpdate(ApplicationDbContext context, IForm before, IForm after)
        {
            if (before is CcBypassInfo)
            {
                var a = (CcBypassInfo) after;
                Is300FootNotice = a.IsFinalMap() || Application is NewConstruction || Application is LotLineAdjustment;
            }
        }

        public override FileUploadProperty[] FileUploadProperties
        {
            get
            {
                FileUploadProperty nnList = new FileUploadProperty(this.Id, NEIGHBORHOOD_NOTICE_LIST_KEY, NEIGHBORHOOD_NOTICE_DIRECTORY, "Neighborhood Notice List");
                return !Is300FootNotice
                    ? new[] {nnList}
                    : new [] { nnList, new FileUploadProperty(this.Id, NEIGHBORHOOD_NOTICE_300_FOOT_RADIUS_KEY, NEIGHBORHOOD_NOTICE_DIRECTORY, "Radius Map", true, true) };

            }
        }

        public override bool CanCopyProperty(PropertyInfo prop)
        {
            return base.CanCopyProperty(prop) && prop.Name != nameof(Is300FootNotice);
        }
    }
}