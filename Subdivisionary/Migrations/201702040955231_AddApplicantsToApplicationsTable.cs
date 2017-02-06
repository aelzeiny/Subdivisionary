namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantsToApplicationsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Application", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.Application", new[] { "ApplicantId" });
            CreateTable(
                "dbo.ApplicantsToApplications",
                c => new
                    {
                        ApplicantRefId = c.Int(nullable: false),
                        ApplicationRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicantRefId, t.ApplicationRefId })
                .ForeignKey("dbo.Applicant", t => t.ApplicantRefId, cascadeDelete: true)
                .ForeignKey("dbo.Application", t => t.ApplicationRefId, cascadeDelete: true)
                .Index(t => t.ApplicantRefId)
                .Index(t => t.ApplicationRefId);
            
            DropColumn("dbo.Application", "ApplicantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "ApplicantId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApplicantsToApplications", "ApplicationRefId", "dbo.Application");
            DropForeignKey("dbo.ApplicantsToApplications", "ApplicantRefId", "dbo.Applicant");
            DropIndex("dbo.ApplicantsToApplications", new[] { "ApplicationRefId" });
            DropIndex("dbo.ApplicantsToApplications", new[] { "ApplicantRefId" });
            DropTable("dbo.ApplicantsToApplications");
            CreateIndex("dbo.Application", "ApplicantId");
            AddForeignKey("dbo.Application", "ApplicantId", "dbo.Applicant", "Id", cascadeDelete: true);
        }
    }
}
