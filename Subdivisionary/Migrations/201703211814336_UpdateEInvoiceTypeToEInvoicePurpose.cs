namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEInvoiceTypeToEInvoicePurpose : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceInfo", "InvoicePurpose", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceInfo", "InvoiceType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceInfo", "InvoiceType", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceInfo", "InvoicePurpose");
        }
    }
}
