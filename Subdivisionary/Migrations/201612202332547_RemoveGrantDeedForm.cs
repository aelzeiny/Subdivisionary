namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGrantDeedForm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AApplication", "GrantDeedForm_List_Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AApplication", "GrantDeedForm_List_Data", c => c.String());
        }
    }
}
