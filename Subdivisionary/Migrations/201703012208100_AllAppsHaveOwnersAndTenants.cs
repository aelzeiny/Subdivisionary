namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllAppsHaveOwnersAndTenants : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "OwnersAndTenants_Data", c => c.String());
            DropColumn("dbo.Application", "EcpOwnersAndTenants_Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "EcpOwnersAndTenants_Data", c => c.String());
            DropColumn("dbo.Application", "OwnersAndTenants_Data");
        }
    }
}
