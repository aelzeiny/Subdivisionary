namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInheritance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AApplication", "ApplicationCheckForm_Checks_Data1", c => c.String());
            AddColumn("dbo.AApplication", "TitleReportForm_ScanPath1", c => c.String());
            AddColumn("dbo.AApplication", "ClosureCalcsForm_ScanPath1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AApplication", "ClosureCalcsForm_ScanPath1");
            DropColumn("dbo.AApplication", "TitleReportForm_ScanPath1");
            DropColumn("dbo.AApplication", "ApplicationCheckForm_Checks_Data1");
        }
    }
}
