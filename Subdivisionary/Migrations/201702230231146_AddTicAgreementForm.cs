namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicAgreementForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "TenantContactsList_Data", c => c.String());
            AddColumn("dbo.Form", "TicAgreementDate", c => c.DateTime());
            AddColumn("dbo.Form", "TicPages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "TicPages");
            DropColumn("dbo.Form", "TicAgreementDate");
            DropColumn("dbo.Form", "TenantContactsList_Data");
        }
    }
}
