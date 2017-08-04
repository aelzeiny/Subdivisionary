namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSignatureUploadInfoWithFinalization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SignatureUploadInfo", "IsSignatureFinalized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SignatureUploadInfo", "IsSignatureFinalized");
        }
    }
}
