namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormIsAssignedBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AApplication", "AdditionalDocumentsForm_IsAssigned", c => c.Boolean(nullable: false));
            AddColumn("dbo.AApplication", "ApplicationCheckForm_IsAssigned", c => c.Boolean());
            AddColumn("dbo.AApplication", "TitleReportForm_IsAssigned", c => c.Boolean());
            AddColumn("dbo.AApplication", "GrantDeedForm_IsAssigned", c => c.Boolean());
            AddColumn("dbo.AApplication", "ClosureCalcsForm_IsAssigned", c => c.Boolean());
            AddColumn("dbo.AApplication", "PhotographForm_IsAssigned", c => c.Boolean());
            AddColumn("dbo.BasicProjectInfo", "ProjectInfo_IsAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasicProjectInfo", "ProjectInfo_IsAssigned");
            DropColumn("dbo.AApplication", "PhotographForm_IsAssigned");
            DropColumn("dbo.AApplication", "ClosureCalcsForm_IsAssigned");
            DropColumn("dbo.AApplication", "GrantDeedForm_IsAssigned");
            DropColumn("dbo.AApplication", "TitleReportForm_IsAssigned");
            DropColumn("dbo.AApplication", "ApplicationCheckForm_IsAssigned");
            DropColumn("dbo.AApplication", "AdditionalDocumentsForm_IsAssigned");
        }
    }
}
