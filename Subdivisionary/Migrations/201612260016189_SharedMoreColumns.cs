namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedMoreColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AApplication", "ClosureCalcsForm_ScanPath");
            RenameColumn(table: "dbo.AApplication", name: "ClosureCalcsForm_ScanPath1", newName: "ClosureCalcsForm_ScanPath");
            AddColumn("dbo.AApplication", "TitleReportForm_Company", c => c.Int());
            AddColumn("dbo.AApplication", "TitleReportForm_Company1", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AApplication", "TitleReportForm_Company1");
            DropColumn("dbo.AApplication", "TitleReportForm_Company");
            RenameColumn(table: "dbo.AApplication", name: "ClosureCalcsForm_ScanPath", newName: "ClosureCalcsForm_ScanPath1");
            AddColumn("dbo.AApplication", "ClosureCalcsForm_ScanPath", c => c.String());
        }
    }
}
