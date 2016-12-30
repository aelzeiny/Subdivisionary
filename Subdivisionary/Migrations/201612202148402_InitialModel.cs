namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AApplication",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectInfoId = c.Int(nullable: false),
                        AdditionalDocumentsForm_AdditionalDocsList_Data = c.String(),
                        ApplicantId = c.Int(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                        ApplicationCheckForm_Checks_Data = c.String(),
                        TitleReportForm_ScanPath = c.String(),
                        GrantDeedForm_List_Data = c.String(),
                        ClosureCalcsForm_ScanPath = c.String(),
                        PhotographForm_ScanPath = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.BasicProjectInfo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                        Block = c.String(nullable: false),
                        Lot = c.String(nullable: false),
                        Address_AddressLine1 = c.String(nullable: false, maxLength: 255),
                        Address_AddressLine2 = c.String(maxLength: 255),
                        Address_City = c.String(nullable: false, maxLength: 255),
                        Address_State = c.String(nullable: false, maxLength: 255),
                        Address_Zip = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_Name = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_Email = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_Phone = c.String(maxLength: 255),
                        PrimaryContactInfo_AddressLine1 = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_AddressLine2 = c.String(maxLength: 255),
                        PrimaryContactInfo_City = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_State = c.String(nullable: false, maxLength: 255),
                        PrimaryContactInfo_Zip = c.String(nullable: false, maxLength: 255),
                        NumberOfUnits = c.Int(),
                        FirmContactInfo_Name = c.String(maxLength: 255),
                        FirmContactInfo_Email = c.String(maxLength: 255),
                        FirmContactInfo_Phone = c.String(maxLength: 255),
                        FirmContactInfo_AddressLine1 = c.String(maxLength: 255),
                        FirmContactInfo_AddressLine2 = c.String(maxLength: 255),
                        FirmContactInfo_City = c.String(maxLength: 255),
                        FirmContactInfo_State = c.String(maxLength: 255),
                        FirmContactInfo_Zip = c.String(maxLength: 255),
                        TenantOccupiedUnits = c.Int(),
                        ResidentialUnits = c.Int(),
                        CommercialUnits = c.Int(),
                        DeveloperContactInfo_Name = c.String(maxLength: 255),
                        DeveloperContactInfo_Email = c.String(maxLength: 255),
                        DeveloperContactInfo_Phone = c.String(maxLength: 255),
                        DeveloperContactInfo_AddressLine1 = c.String(maxLength: 255),
                        DeveloperContactInfo_AddressLine2 = c.String(maxLength: 255),
                        DeveloperContactInfo_City = c.String(maxLength: 255),
                        DeveloperContactInfo_State = c.String(maxLength: 255),
                        DeveloperContactInfo_Zip = c.String(maxLength: 255),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AApplication", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DataId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicant", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AApplication", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.BasicProjectInfo", "Id", "dbo.AApplication");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BasicProjectInfo", new[] { "Id" });
            DropIndex("dbo.AApplication", new[] { "ApplicantId" });
            DropIndex("dbo.Applicant", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BasicProjectInfo");
            DropTable("dbo.AApplication");
            DropTable("dbo.Applicant");
        }
    }
}
