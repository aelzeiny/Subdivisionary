namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTenantIntentToPurchase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "IntentToPurchaseTenantUnit", c => c.String());
            AddColumn("dbo.Form", "IntentToPurchaseTenantAddress", c => c.String());
            AddColumn("dbo.Form", "IntentToPurchaseTenantSalePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Form", "SquareFeet", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Form", "CurrentRentalRate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Form", "ProposedSalesPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Form", "ProposedSalesPrice", c => c.Single());
            AlterColumn("dbo.Form", "CurrentRentalRate", c => c.Single());
            AlterColumn("dbo.Form", "SquareFeet", c => c.Single());
            DropColumn("dbo.Form", "IntentToPurchaseTenantSalePrice");
            DropColumn("dbo.Form", "IntentToPurchaseTenantAddress");
            DropColumn("dbo.Form", "IntentToPurchaseTenantUnit");
        }
    }
}
