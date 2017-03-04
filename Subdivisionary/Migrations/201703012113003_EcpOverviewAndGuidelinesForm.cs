namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EcpOverviewAndGuidelinesForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "UserAgreesToEcpGuidelines", c => c.Boolean());
            AddColumn("dbo.Form", "UserAgreesToEcpTerms", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "UserAgreesToEcpTerms");
            DropColumn("dbo.Form", "UserAgreesToEcpGuidelines");
        }
    }
}
