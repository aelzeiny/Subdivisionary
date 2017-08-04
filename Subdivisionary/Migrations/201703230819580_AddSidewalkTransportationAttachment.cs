namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkTransportationAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "HasMtaReviewedPlans", c => c.Boolean());
            AddColumn("dbo.Form", "MtaNameSectionDateList_Data", c => c.String());
            AddColumn("dbo.Form", "HasTascReviewedPlans", c => c.Boolean());
            AddColumn("dbo.Form", "MtaDateOfTascHearing", c => c.DateTime());
            AddColumn("dbo.Form", "MtaDateOfTascApproval", c => c.DateTime());
            AddColumn("dbo.Form", "WillChangeStreetParking", c => c.Boolean());
            AddColumn("dbo.Form", "ParkingSpotsRemoved", c => c.Int());
            AddColumn("dbo.Form", "ProposedCurbColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "ProposedCurbColor");
            DropColumn("dbo.Form", "ParkingSpotsRemoved");
            DropColumn("dbo.Form", "WillChangeStreetParking");
            DropColumn("dbo.Form", "MtaDateOfTascApproval");
            DropColumn("dbo.Form", "MtaDateOfTascHearing");
            DropColumn("dbo.Form", "HasTascReviewedPlans");
            DropColumn("dbo.Form", "MtaNameSectionDateList_Data");
            DropColumn("dbo.Form", "HasMtaReviewedPlans");
        }
    }
}
