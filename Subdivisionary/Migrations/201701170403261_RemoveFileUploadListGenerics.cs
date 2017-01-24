namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFileUploadListGenerics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "ChecksUploadList_Data", c => c.String());
            DropColumn("dbo.Form", "Checks_Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "Checks_Data", c => c.String());
            DropColumn("dbo.Form", "ChecksUploadList_Data");
        }
    }
}
