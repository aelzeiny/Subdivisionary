namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationStatusLogItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationStatusLogItem", "Application_Id", "dbo.Application");
            DropIndex("dbo.ApplicationStatusLogItem", new[] { "Application_Id" });
            DropTable("dbo.ApplicationStatusLogItem");
        }
    }
}
