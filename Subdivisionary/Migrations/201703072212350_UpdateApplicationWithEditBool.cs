namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationWithEditBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "CanEdit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "CanEdit");
        }
    }
}
