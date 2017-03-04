namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoticeToTenants : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "AffirmedNoticeToAllTenants", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "AffirmedNoticeToAllTenants");
        }
    }
}
