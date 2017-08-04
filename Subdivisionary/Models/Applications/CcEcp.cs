using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.Models.Applications
{
    public class CcEcp : Application
    {
        protected override void Init()
        {
            ProjectInfo = new CcEcpInfo();
        }

        protected override Form[] GetDefaultApplicationForms()
        {
            return new Form[]
            {
                new EcpOverviewForm(),
                new EcpGuidelinesForm(),
                new OwnerForm(),
                new TenantForm() {IsRequired = false},
                new CoverLetterForm(),
                new TentativeMapForm(),
                new TitleReportForm(),
                new GrantDeedForm(), 
                new RecordedMapsOnBlock(), 
                new DbiPhysicalInspection(), 
                new BuildingHistory(),
                new Report3R(), 
                new ProofOfOwnerOccupancy(),
                new AcknowledgmentOfFees(), 
                new OwnersReleaseOfInterestInCommonAreas(), 
                new TentativeMapForm(), 
                new NoticeToTenants(),
                new SummaryOfTenantContacts() {IsRequired = false},
                new SubdividersStatementToExistingTenants(), 
                new SubdividersStatementToNewTenants(), 
                new TenantRightOfRefusal(),
                new PhotographForm(), 
                new PropMFindingsForm(), 
                new OwnersAffidavitOfTentantEvictions(), 
                new TicAgreement()
            };
        }

        public override void FormUpdated(ApplicationDbContext context, IForm before, IForm after)
        {
            if (after is CcEcpInfo)
            {
                var a = (CcEcpInfo) after;
                // This is tenantForm special case-scenario for ECP Projects.
                // When the number of units is added or reduced we add or reduce the number of UnitHistory Forms
                var unitForms = this.Forms.Where(x => x is UnitHistoryForm).ToArray();

                if (unitForms.Length < a.NumberOfUnits)
                {
                    for (int i = unitForms.Length; i < a.NumberOfUnits; i++)
                        this.Forms.Add(new UnitHistoryForm());
                }
                else if (unitForms.Length > a.NumberOfUnits)
                {
                    for (int i = 0; i < unitForms.Length - a.NumberOfUnits; i++)
                    {
                        var toRemove = unitForms[unitForms.Length - i - 1];
                        context.Forms.Remove(toRemove);
                        this.Forms.Remove(toRemove);
                    }
                }
            }

            else if (after is TenantForm)
                SyncTenantIntentToPurchase(context, (TenantForm)after);

            base.FormUpdated(context, before, after);
        }

        private void SyncTenantIntentToPurchase(ApplicationDbContext context, TenantForm tenantForm)
        {
            // Ensure that TenantIntentToPurchase Forms are added as needed
            foreach (var t in tenantForm.TenantsList)
            {
                if (t.TenantIntentToPurchase)
                {
                    // If tenant intends to purchase tenantForm unit, but the application doesn't have tenantForm purchase form, then add the form
                    if (
                        !Forms.Any(
                            x =>
                                x is TenantIntentToPurchase &&
                                ((TenantIntentToPurchase)x).IntentToPurchaseTenantName == t.TenantName))
                    {
                        Forms.Add(new TenantIntentToPurchase() { IntentToPurchaseTenantName = t.TenantName, IsRequired = false });
                    }
                    // If tenant does intend to purchase unit, but the application has an offer form, then delete the form
                    var form =
                        Forms.FirstOrDefault(
                            x =>
                                x is TenantIntentToAcceptOfferOfLtl &&
                                ((TenantIntentToAcceptOfferOfLtl)x).IntentToAcceptOfferName == t.TenantName);
                    if (form != null)
                    {
                        Forms.Remove(form);
                        context.Forms.Remove(form);
                    }
                }
                else
                {
                    // If tenant intends to purchase tenantForm unit, but the application doesn't have an form, then add the offer form

                    if (
                        !Forms.Any(
                            x =>
                                x is TenantIntentToAcceptOfferOfLtl &&
                                ((TenantIntentToAcceptOfferOfLtl)x).IntentToAcceptOfferName == t.TenantName))
                    {
                        Forms.Add(new TenantIntentToPurchase() { IntentToPurchaseTenantName = t.TenantName, IsRequired = false });
                    }
                    // If tenant does not intend to purchase unit, but the application has tenantForm purchase form for that tenant, then delete form
                    var form =
                        Forms.FirstOrDefault(
                            x =>
                                x is TenantIntentToPurchase &&
                                ((TenantIntentToPurchase)x).IntentToPurchaseTenantName == t.TenantName);
                    if (form != null)
                    {
                        Forms.Remove(form);
                        context.Forms.Remove(form);
                    }
                }
            }
        }

        public override IEnumerable<string> NextSteps()
        {
            return new[]
            {
                "The original copy of the 'Affidavit for Ownership/Occupancy' (Form 11)",
                "Some other example of a notary goes here",
                "I don't know. Lorem Ipsum. To be added & discussed later"
            };
        }


        public override IList<ValidationException> Review()
        {
            IList<ValidationException> answer = new List<ValidationException>();
            // Ensure 40% of applicants have signed the intent to purchase form
            var projectInfo = (CcEcpInfo)ProjectInfo;
            var requiredTenantAgreements = Math.Round(.4f * projectInfo.NumberOfUnits);
            int numberofSignedTenants =
                Forms.Count(x => (x is TenantIntentToPurchase || x is TenantIntentToAcceptOfferOfLtl) && x.IsAssigned);
            if (numberofSignedTenants < requiredTenantAgreements)
            {
                answer.Add(new ValidationException($"Currently only {requiredTenantAgreements} have either signed their intent to purchase" +
                                                " the unit or accept their offer of lifetime lease. " +
                                                $"This application requires at least {requiredTenantAgreements}"));
            }

            return answer;
        }


        public override EFeeSchedule PaymentSchedule => EFeeSchedule.Conversions;

        public override string DisplayName => "Expedited Conversion Program";
    }
}