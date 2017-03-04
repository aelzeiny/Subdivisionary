namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectInfosToIncludeContactBools : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasicProjectInfo", "OwnerAndPrimaryContactAreSame", c => c.Boolean(nullable: false));
            AddColumn("dbo.BasicProjectInfo", "OwnerAndLandDevContactAreSame", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasicProjectInfo", "OwnerAndLandDevContactAreSame");
            DropColumn("dbo.BasicProjectInfo", "OwnerAndPrimaryContactAreSame");
        }
    }
}
