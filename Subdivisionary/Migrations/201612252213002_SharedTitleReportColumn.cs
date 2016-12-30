namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedTitleReportColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AApplication", "TitleReportForm_ScanPath");
            RenameColumn(table: "dbo.AApplication", name: "TitleReportForm_ScanPath1", newName: "TitleReportForm_ScanPath");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AApplication", name: "TitleReportForm_ScanPath", newName: "TitleReportForm_ScanPath1");
            AddColumn("dbo.AApplication", "TitleReportForm_ScanPath", c => c.String());
        }
    }
}
