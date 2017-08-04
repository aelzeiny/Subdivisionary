namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkBsmAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "HasSubmittedStreetInprovementPermit", c => c.Boolean());
            AddColumn("dbo.Form", "BsmPermitNameList_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "BsmPermitNameList_Data");
            DropColumn("dbo.Form", "HasSubmittedStreetInprovementPermit");
        }
    }
}
