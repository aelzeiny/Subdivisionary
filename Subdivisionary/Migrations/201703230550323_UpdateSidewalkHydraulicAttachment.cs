namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSidewalkHydraulicAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "SidewalkProposalReviewedByHydraulicEngrs", c => c.Boolean());
            AddColumn("dbo.Form", "SidewalkProposalMeetsHundredYrRequirements", c => c.Boolean());
            DropColumn("dbo.Form", "ProposalMeetsHundredYrRequirements");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "ProposalMeetsHundredYrRequirements", c => c.Boolean());
            DropColumn("dbo.Form", "SidewalkProposalMeetsHundredYrRequirements");
            DropColumn("dbo.Form", "SidewalkProposalReviewedByHydraulicEngrs");
        }
    }
}
