namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCheckList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "ChecksList_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "ChecksList_Data");
        }
    }
}
