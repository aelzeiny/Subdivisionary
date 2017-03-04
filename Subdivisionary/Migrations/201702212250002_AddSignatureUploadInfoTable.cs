namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignatureUploadInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SignatureUploadInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SignatureFormId = c.Int(nullable: false),
                        DateStamp = c.DateTime(nullable: false),
                        UserStamp = c.String(),
                        SignerName = c.String(),
                        DataFormat = c.String(),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Form", t => t.SignatureFormId, cascadeDelete: true)
                .Index(t => t.SignatureFormId);
            
            AddColumn("dbo.Application", "EcpOwnersAndTenants_Data", c => c.String());
            AddColumn("dbo.Form", "SignatureUploadProperties_Data", c => c.String());
            AddColumn("dbo.Form", "StatementOfRepairsAndImprovements", c => c.String());
            AddColumn("dbo.Form", "StatementOfBuildingHistory", c => c.String());
            AddColumn("dbo.Form", "PermitList_Data", c => c.String());
            AddColumn("dbo.Form", "PropMDate", c => c.DateTime());
            AddColumn("dbo.Form", "CityPlanningCaseNo", c => c.Int());
            AddColumn("dbo.Form", "PropMQuestion1", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion2", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion3", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion4", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion5", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion6", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion7", c => c.String());
            AddColumn("dbo.Form", "PropMQuestion8", c => c.String());
            AddColumn("dbo.Form", "TenantsList_Data", c => c.String());
            AddColumn("dbo.Form", "ApartmentNumber", c => c.String());
            AddColumn("dbo.Form", "OccupancyType", c => c.Int());
            AddColumn("dbo.Form", "NumberOfBedrooms", c => c.Int());
            AddColumn("dbo.Form", "SquareFeet", c => c.Single());
            AddColumn("dbo.Form", "CurrentRentalRate", c => c.Single());
            AddColumn("dbo.Form", "ProposedSalesPrice", c => c.Single());
            AddColumn("dbo.Form", "Timeline_Data", c => c.String());
            AddColumn("dbo.Form", "OccupiedNamesList_Data", c => c.String());
            AddColumn("dbo.Form", "OccupancyRangeOccupationHistoryList_Data", c => c.String());
            AddColumn("dbo.Form", "Is300FootNotice", c => c.Boolean());
            AddColumn("dbo.Form", "IsVestingMap", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignatureUploadInfo", "SignatureFormId", "dbo.Form");
            DropIndex("dbo.SignatureUploadInfo", new[] { "SignatureFormId" });
            DropColumn("dbo.Form", "IsVestingMap");
            DropColumn("dbo.Form", "Is300FootNotice");
            DropColumn("dbo.Form", "OccupancyRangeOccupationHistoryList_Data");
            DropColumn("dbo.Form", "OccupiedNamesList_Data");
            DropColumn("dbo.Form", "Timeline_Data");
            DropColumn("dbo.Form", "ProposedSalesPrice");
            DropColumn("dbo.Form", "CurrentRentalRate");
            DropColumn("dbo.Form", "SquareFeet");
            DropColumn("dbo.Form", "NumberOfBedrooms");
            DropColumn("dbo.Form", "OccupancyType");
            DropColumn("dbo.Form", "ApartmentNumber");
            DropColumn("dbo.Form", "TenantsList_Data");
            DropColumn("dbo.Form", "PropMQuestion8");
            DropColumn("dbo.Form", "PropMQuestion7");
            DropColumn("dbo.Form", "PropMQuestion6");
            DropColumn("dbo.Form", "PropMQuestion5");
            DropColumn("dbo.Form", "PropMQuestion4");
            DropColumn("dbo.Form", "PropMQuestion3");
            DropColumn("dbo.Form", "PropMQuestion2");
            DropColumn("dbo.Form", "PropMQuestion1");
            DropColumn("dbo.Form", "CityPlanningCaseNo");
            DropColumn("dbo.Form", "PropMDate");
            DropColumn("dbo.Form", "PermitList_Data");
            DropColumn("dbo.Form", "StatementOfBuildingHistory");
            DropColumn("dbo.Form", "StatementOfRepairsAndImprovements");
            DropColumn("dbo.Form", "SignatureUploadProperties_Data");
            DropColumn("dbo.Application", "EcpOwnersAndTenants_Data");
            DropTable("dbo.SignatureUploadInfo");
        }
    }
}
