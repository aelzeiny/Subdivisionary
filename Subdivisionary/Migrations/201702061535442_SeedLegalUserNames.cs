namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedLegalUserNames : DbMigration
    {
        public override void Up()
        {
Sql(@"
UPDATE [dbo].[AspNetUsers] SET [Name]=N'Ahmed Elzeiny' WHERE [UserName] = N'ahmed.elzeiny2@sfdpw.org'
UPDATE [dbo].[AspNetUsers] SET [Name]=N'Subdivision Mapping' WHERE [UserName] = N'subdivision.mapping@sfdpw.org'
UPDATE [dbo].[AspNetUsers] SET [Name]=N'John Doe' WHERE [UserName] = N'applicant@sfdpw.org'
");
        }
        
        public override void Down()
        {
        }
    }
}
