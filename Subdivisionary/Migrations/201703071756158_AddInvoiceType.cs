namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceInfo", "InvoiceType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceInfo", "InvoiceType");
        }
    }
}
