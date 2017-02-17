namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignatureUploadInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SignatureUploadInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                        DateStamp = c.DateTime(nullable: false),
                        UserStamp = c.String(),
                        SignerName = c.String(),
                        DataFormat = c.String(),
                        Data = c.String(),
                        OwnersAffidavitOfTentantEvictions_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Form", t => t.FormId, cascadeDelete: true)
                .ForeignKey("dbo.Form", t => t.OwnersAffidavitOfTentantEvictions_Id)
                .Index(t => t.FormId)
                .Index(t => t.OwnersAffidavitOfTentantEvictions_Id);
            
            AddColumn("dbo.Form", "SignatureUploadProperties_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignatureUploadInfo", "OwnersAffidavitOfTentantEvictions_Id", "dbo.Form");
            DropForeignKey("dbo.SignatureUploadInfo", "FormId", "dbo.Form");
            DropIndex("dbo.SignatureUploadInfo", new[] { "OwnersAffidavitOfTentantEvictions_Id" });
            DropIndex("dbo.SignatureUploadInfo", new[] { "FormId" });
            DropColumn("dbo.Form", "SignatureUploadProperties_Data");
            DropTable("dbo.SignatureUploadInfo");
        }
    }
}
