namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBuildingAndUnitHistoryForms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "StatementOfRepairsAndImprovements", c => c.String());
            AddColumn("dbo.Form", "PermitList_Data", c => c.String());
            AddColumn("dbo.Form", "UnitHistoryNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "UnitHistoryNumber");
            DropColumn("dbo.Form", "PermitList_Data");
            DropColumn("dbo.Form", "StatementOfRepairsAndImprovements");
        }
    }
}
