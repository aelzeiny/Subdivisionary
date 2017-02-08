namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "Owners_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "Owners_Data");
        }
    }
}
