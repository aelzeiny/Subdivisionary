namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEApplicationStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationStatusLogItem", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationStatusLogItem", "Status", c => c.String());
        }
    }
}
