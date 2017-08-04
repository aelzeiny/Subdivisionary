namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkHydraulicAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "HydraulicDateNameList_Data", c => c.String());
            AddColumn("dbo.Form", "ProposalMeetsHundredYrRequirements", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "ProposalMeetsHundredYrRequirements");
            DropColumn("dbo.Form", "HydraulicDateNameList_Data");
        }
    }
}
