namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectInfoAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasicProjectInfo", "AddressList_Data", c => c.String());
            DropColumn("dbo.BasicProjectInfo", "Block");
            DropColumn("dbo.BasicProjectInfo", "Lot");
            DropColumn("dbo.BasicProjectInfo", "Address_AddressLine1");
            DropColumn("dbo.BasicProjectInfo", "Address_AddressLine2");
            DropColumn("dbo.BasicProjectInfo", "Address_City");
            DropColumn("dbo.BasicProjectInfo", "Address_State");
            DropColumn("dbo.BasicProjectInfo", "Address_Zip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasicProjectInfo", "Address_Zip", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "Address_State", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "Address_City", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "Address_AddressLine2", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "Address_AddressLine1", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "Lot", c => c.String(nullable: false));
            AddColumn("dbo.BasicProjectInfo", "Block", c => c.String(nullable: false));
            DropColumn("dbo.BasicProjectInfo", "AddressList_Data");
        }
    }
}
