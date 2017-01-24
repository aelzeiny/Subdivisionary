namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTitleReportForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "PtrFile_Data", c => c.String());
            AddColumn("dbo.Form", "PtrContactList_Data", c => c.String());
            AddColumn("dbo.Form", "OrderNumber", c => c.String());
            DropColumn("dbo.Form", "ScanPath1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "ScanPath1", c => c.String());
            DropColumn("dbo.Form", "OrderNumber");
            DropColumn("dbo.Form", "PtrContactList_Data");
            DropColumn("dbo.Form", "PtrFile_Data");
        }
    }
}
