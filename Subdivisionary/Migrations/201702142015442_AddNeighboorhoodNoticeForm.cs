namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNeighboorhoodNoticeForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "Is300FootNotice", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "Is300FootNotice");
        }
    }
}
