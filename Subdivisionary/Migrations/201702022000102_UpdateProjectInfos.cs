namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectInfos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "DanielSampleString", c => c.String());
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Name", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Email", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Phone", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_AddressLine1", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_AddressLine2", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_City", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_State", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Zip", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "CreatesVerticalSubdivision", c => c.Boolean());
            AddColumn("dbo.BasicProjectInfo", "HasExistingBuilding", c => c.Boolean());
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Name", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Email", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Phone", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_AddressLine1", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_AddressLine2", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_City", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_State", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Zip", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "NumOfExisitingLots", c => c.Int());
            AddColumn("dbo.BasicProjectInfo", "NumOfProposedLots", c => c.Int());
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_Name");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_Email");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_Phone");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_AddressLine1");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_AddressLine2");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_City");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_State");
            DropColumn("dbo.BasicProjectInfo", "FirmContactInfo_Zip");
            DropColumn("dbo.BasicProjectInfo", "TenantOccupiedUnits");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasicProjectInfo", "TenantOccupiedUnits", c => c.Int());
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_Zip", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_State", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_City", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_AddressLine2", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_AddressLine1", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_Phone", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_Email", c => c.String(maxLength: 255));
            AddColumn("dbo.BasicProjectInfo", "FirmContactInfo_Name", c => c.String(maxLength: 255));
            DropColumn("dbo.BasicProjectInfo", "NumOfProposedLots");
            DropColumn("dbo.BasicProjectInfo", "NumOfExisitingLots");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Zip");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_State");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_City");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_AddressLine2");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_AddressLine1");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Phone");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Email");
            DropColumn("dbo.BasicProjectInfo", "LandFirmContactInfo_Name");
            DropColumn("dbo.BasicProjectInfo", "HasExistingBuilding");
            DropColumn("dbo.BasicProjectInfo", "CreatesVerticalSubdivision");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Zip");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_State");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_City");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_AddressLine2");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_AddressLine1");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Phone");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Email");
            DropColumn("dbo.BasicProjectInfo", "OwnerContactInfo_Name");
            DropColumn("dbo.Form", "DanielSampleString");
        }
    }
}
