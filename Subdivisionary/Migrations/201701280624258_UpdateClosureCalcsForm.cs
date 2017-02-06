namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClosureCalcsForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "ClosureCalcsFiles_Data", c => c.String());
            DropColumn("dbo.Form", "ScanPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "ScanPath", c => c.String());
            DropColumn("dbo.Form", "ClosureCalcsFiles_Data");
        }
    }
}
