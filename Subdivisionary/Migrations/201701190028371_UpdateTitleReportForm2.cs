namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTitleReportForm2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "TitleCompany", c => c.Int());
            AddColumn("dbo.Form", "OtherTitleCompany", c => c.String());
            DropColumn("dbo.Form", "Company");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Form", "Company", c => c.Int());
            DropColumn("dbo.Form", "OtherTitleCompany");
            DropColumn("dbo.Form", "TitleCompany");
        }
    }
}
