namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationIsSubmitted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Application", "IsSubmitted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "IsSubmitted", c => c.Boolean(nullable: false));
        }
    }
}
