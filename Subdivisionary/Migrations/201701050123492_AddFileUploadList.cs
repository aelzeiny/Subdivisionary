namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileUploadList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "GrantPiqFile_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "GrantPiqFile_Data");
        }
    }
}
