namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileUploadInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileUploadInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                        Url = c.String(),
                        Size = c.Long(nullable: false),
                        Type = c.String(),
                        FileKey = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Form", t => t.FormId, cascadeDelete: true)
                .Index(t => t.FormId);
            
            DropColumn("dbo.Form", "ChecksUploadList_Data");
            DropColumn("dbo.Form", "ClosureCalcsFiles_Data");
            DropColumn("dbo.Form", "DanielSampleString");
            DropColumn("dbo.Form", "GrantPiqFile_Data");
            DropColumn("dbo.Form", "GrantAdjoinerFiles_Data");
            DropColumn("dbo.Form", "PhotoLeft_Data");
            DropColumn("dbo.Form", "PhotoRight_Data");
            DropColumn("dbo.Form", "PhotoFront_Data");
            DropColumn("dbo.Form", "PhotoBack_Data");
            DropColumn("dbo.Form", "PtrFile_Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "PtrFile_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoBack_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoFront_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoRight_Data", c => c.String());
            AddColumn("dbo.Form", "PhotoLeft_Data", c => c.String());
            AddColumn("dbo.Form", "GrantAdjoinerFiles_Data", c => c.String());
            AddColumn("dbo.Form", "GrantPiqFile_Data", c => c.String());
            AddColumn("dbo.Form", "DanielSampleString", c => c.String());
            AddColumn("dbo.Form", "ClosureCalcsFiles_Data", c => c.String());
            AddColumn("dbo.Form", "ChecksUploadList_Data", c => c.String());
            DropForeignKey("dbo.FileUploadInfo", "FormId", "dbo.Form");
            DropIndex("dbo.FileUploadInfo", new[] { "FormId" });
            DropTable("dbo.FileUploadInfo");
        }
    }
}
