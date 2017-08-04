namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        ApplicationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.Notification", new[] { "ApplicantId" });
            DropTable("dbo.Notification");
        }
    }
}
