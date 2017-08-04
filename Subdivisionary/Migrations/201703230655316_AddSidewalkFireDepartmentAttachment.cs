namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkFireDepartmentAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "HasFdReviewedPlans", c => c.Boolean());
            AddColumn("dbo.Form", "FdSidewalkReviewers_Data", c => c.String());
            AddColumn("dbo.Form", "HasFdFireHydrants", c => c.Boolean());
            AddColumn("dbo.Form", "FdPressureLocationList_Data", c => c.String());
            AddColumn("dbo.Form", "FdLocationWidthList_Data", c => c.String());
            AddColumn("dbo.Form", "FdLocationHeightList_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "FdLocationHeightList_Data");
            DropColumn("dbo.Form", "FdLocationWidthList_Data");
            DropColumn("dbo.Form", "FdPressureLocationList_Data");
            DropColumn("dbo.Form", "HasFdFireHydrants");
            DropColumn("dbo.Form", "FdSidewalkReviewers_Data");
            DropColumn("dbo.Form", "HasFdReviewedPlans");
        }
    }
}
