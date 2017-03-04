namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDbiInspectionForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "AreaOfWalls", c => c.Single());
            AddColumn("dbo.Form", "AreaOfOpenings", c => c.Single());
            AddColumn("dbo.Form", "ConstructionMaterial", c => c.String());
            AddColumn("dbo.Form", "Other", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "Other");
            DropColumn("dbo.Form", "ConstructionMaterial");
            DropColumn("dbo.Form", "AreaOfOpenings");
            DropColumn("dbo.Form", "AreaOfWalls");
        }
    }
}
