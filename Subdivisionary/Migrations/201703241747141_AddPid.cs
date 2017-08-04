namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "Pid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "Pid");
        }
    }
}
