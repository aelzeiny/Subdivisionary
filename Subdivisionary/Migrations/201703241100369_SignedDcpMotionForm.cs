namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SignedDcpMotionForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "MotionNo", c => c.String());
            AddColumn("dbo.Form", "FileNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "FileNo");
            DropColumn("dbo.Form", "MotionNo");
        }
    }
}
