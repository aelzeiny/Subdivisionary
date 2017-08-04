namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoicesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceInfo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PaymentFormId = c.Int(nullable: false),
                        PayUrl = c.String(),
                        PrintUrl = c.String(),
                        Paid = c.Boolean(nullable: false),
                        Void = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Amount = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Form", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            AddColumn("dbo.Form", "PaidWithChecks", c => c.Boolean());
            AddColumn("dbo.Form", "InvoiceId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceInfo", "Id", "dbo.Form");
            DropIndex("dbo.InvoiceInfo", new[] { "Id" });
            DropColumn("dbo.Form", "InvoiceId");
            DropColumn("dbo.Form", "PaidWithChecks");
            DropTable("dbo.InvoiceInfo");
        }
    }
}
