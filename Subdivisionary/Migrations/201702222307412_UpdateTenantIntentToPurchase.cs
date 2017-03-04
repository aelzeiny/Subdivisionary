namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTenantIntentToPurchase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "IntentToAcceptAddress", c => c.String());
            AddColumn("dbo.Form", "IntentToAcceptTenantUnit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "IntentToAcceptTenantUnit");
            DropColumn("dbo.Form", "IntentToAcceptAddress");
        }
    }
}
