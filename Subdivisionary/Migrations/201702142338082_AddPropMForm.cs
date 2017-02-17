namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropMForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "PropMDate", c => c.DateTime());
            AddColumn("dbo.Form", "CityPlanningCaseNo", c => c.Int());
            AddColumn("dbo.Form", "PropMQuestion1", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion2", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion3", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion4", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion5", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion6", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion7", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion8", c => c.String());
            AddColumn("dbo.Form", "SignatureUploadProperties_Data1", c => c.String());
            AddColumn("dbo.SignatureUploadInfo", "PropMFindingsForm_Id", c => c.Int());
            CreateIndex("dbo.SignatureUploadInfo", "PropMFindingsForm_Id");
            AddForeignKey("dbo.SignatureUploadInfo", "PropMFindingsForm_Id", "dbo.Form", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignatureUploadInfo", "PropMFindingsForm_Id", "dbo.Form");
            DropIndex("dbo.SignatureUploadInfo", new[] { "PropMFindingsForm_Id" });
            DropColumn("dbo.SignatureUploadInfo", "PropMFindingsForm_Id");
            DropColumn("dbo.Form", "SignatureUploadProperties_Data1");
            DropColumn("dbo.Form", "PropMQuestion8");
            DropColumn("dbo.Form", "PropMQuestion7");
            DropColumn("dbo.Form", "PropMQuestion6");
            DropColumn("dbo.Form", "PropMQuestion5");
            DropColumn("dbo.Form", "PropMQuestion4");
            DropColumn("dbo.Form", "PropMQuestion3");
            DropColumn("dbo.Form", "PropMQuestion2");
            DropColumn("dbo.Form", "PropMQuestion1");
            DropColumn("dbo.Form", "CityPlanningCaseNo");
            DropColumn("dbo.Form", "PropMDate");
        }
    }
}
