namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSidewalkAndPmFmGuidelines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "ReadAndAffirmPmAndFmApp", c => c.Boolean());
            AddColumn("dbo.Form", "ReadAndAffirmSidewalkApp", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "ReadAndAffirmSidewalkApp");
            DropColumn("dbo.Form", "ReadAndAffirmPmAndFmApp");
        }
    }
}
