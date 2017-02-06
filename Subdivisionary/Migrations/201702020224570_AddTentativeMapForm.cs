namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTentativeMapForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "IsFinalMap", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "IsFinalMap");
        }
    }
}
