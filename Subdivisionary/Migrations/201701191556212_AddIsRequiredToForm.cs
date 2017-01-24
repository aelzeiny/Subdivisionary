namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsRequiredToForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "IsRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "IsRequired");
        }
    }
}
