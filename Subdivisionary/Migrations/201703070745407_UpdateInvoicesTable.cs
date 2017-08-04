namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoicesTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InvoiceInfo", new[] { "Id" });
            DropPrimaryKey("dbo.InvoiceInfo");
            AddColumn("dbo.InvoiceInfo", "InvoiceNo", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceInfo", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.InvoiceInfo", "Id");
            CreateIndex("dbo.InvoiceInfo", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InvoiceInfo", new[] { "Id" });
            DropPrimaryKey("dbo.InvoiceInfo");
            AlterColumn("dbo.InvoiceInfo", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceInfo", "InvoiceNo");
            AddPrimaryKey("dbo.InvoiceInfo", "Id");
            CreateIndex("dbo.InvoiceInfo", "Id");
        }
    }
}
