namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhotographModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Form", "ScanPath1");
            RenameColumn(table: "dbo.Form", name: "ScanPath2", newName: "ScanPath1");
            AddColumn("dbo.Form", "PhotoLeft_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoRight_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoFront_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoBack_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "PhotoBack_Data");
            DropColumn("dbo.Form", "PhotoFront_Data");
            DropColumn("dbo.Form", "PhotoRight_Data");
            DropColumn("dbo.Form", "PhotoLeft_Data");
            RenameColumn(table: "dbo.Form", name: "ScanPath1", newName: "ScanPath2");
            AddColumn("dbo.Form", "ScanPath1", c => c.String());
        }
    }
}
