namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShareRequestsToApplications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "SharedRequests_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "SharedRequests_Data");
        }
    }
}
