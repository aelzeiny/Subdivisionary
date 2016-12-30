namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddGrantDeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AApplication", "GrantDeedForm_List_Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AApplication", "GrantDeedForm_List_Data");
        }
    }
}
