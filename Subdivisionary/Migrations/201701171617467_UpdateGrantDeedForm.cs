namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGrantDeedForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "ApnList_Data", c => c.String());
            DropColumn("dbo.Form", "List_Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "List_Data", c => c.String());
            DropColumn("dbo.Form", "ApnList_Data");
        }
    }
}
