namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkDcpAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "DcpReviewedProject", c => c.Boolean());
            AddColumn("dbo.Form", "DcpDateOfReview", c => c.DateTime());
            AddColumn("dbo.Form", "DcpCaseNoReview", c => c.String());
            AddColumn("dbo.Form", "DcpReviewerName", c => c.String());
            AddColumn("dbo.Form", "DcpHaveGeneralReferralApproval", c => c.Boolean());
            AddColumn("dbo.Form", "DcpDateOfReferralApproval", c => c.DateTime());
            AddColumn("dbo.Form", "DcpCaseNoReferralApproval", c => c.String());
            AddColumn("dbo.Form", "DcpCeqaClearance", c => c.Boolean());
            AddColumn("dbo.Form", "DcpDateOfCeqaApproval", c => c.DateTime());
            AddColumn("dbo.Form", "DcpCaseNoCeqaApproval", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "DcpCaseNoCeqaApproval");
            DropColumn("dbo.Form", "DcpDateOfCeqaApproval");
            DropColumn("dbo.Form", "DcpCeqaClearance");
            DropColumn("dbo.Form", "DcpCaseNoReferralApproval");
            DropColumn("dbo.Form", "DcpDateOfReferralApproval");
            DropColumn("dbo.Form", "DcpHaveGeneralReferralApproval");
            DropColumn("dbo.Form", "DcpReviewerName");
            DropColumn("dbo.Form", "DcpCaseNoReview");
            DropColumn("dbo.Form", "DcpDateOfReview");
            DropColumn("dbo.Form", "DcpReviewedProject");
        }
    }
}
