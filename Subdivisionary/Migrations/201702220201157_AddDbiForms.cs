namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDbiForms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "Report3RIssuedDate", c => c.DateTime());
            AddColumn("dbo.Form", "FamilyDwellingSize", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "FamilyDwellingSize");
            DropColumn("dbo.Form", "Report3RIssuedDate");
        }
    }
}
