using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class ParcelFinalMap : Application
    {
        protected override void Init()
        {
            ProjectInfo = new LotMergerAndSubdivisionInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new ParcelOrFinalMapGuidelines(),
                new OwnerForm(),
                new TentativeMapForm(),
                new GrantDeedForm(),
                new Report3R(),
                new NeighborhoodNotificationForm(),
                new PropMFindingsForm(), 
                new PhotographForm(), 
                new DbiInspectionRequirementsForm() {IsRequired = false}
            };
        }

        public override EFeeSchedule PaymentSchedule => EFeeSchedule.ParcelFinalMap;

        public override string DisplayName => (ProjectInfo != null) ? 
            (((LotMergerAndSubdivisionInfo)ProjectInfo).NumOfProposedLots <= MAX_PARCEL_MAP_UNITS ? "Parcel Map" : "Final Map") 
            : "Parcel/Final Map";
    }
}