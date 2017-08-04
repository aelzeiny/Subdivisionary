namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [DataId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'78f5cd5b-2e47-4aac-8539-6ee8879cc445', 2, N'ahmed.elzeiny2@sfdpw.org', 0, N'AIDK1SErklINwEVLfSzaiy7OIbuQsB4j9DJSlaFPH4PE9eDfzEcPQDxaXepHAbqkJg==', N'72a550ac-db0a-453b-9a07-16ca6b30ef96', NULL, 0, 0, NULL, 1, 0, N'ahmed.elzeiny2@sfdpw.org')
INSERT INTO [dbo].[AspNetUsers] ([Id], [DataId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7fc32ec4-2d0b-43e1-a8c3-44f17c418fab', 1, N'subdivision.mapping@sfdpw.org', 0, N'APbEybvqkiutAdgdHnqhcXyKgz+SOuRjzceRjMoll5k8E3f4CEGiYUSurpX/YX9nWQ==', N'1b0eb7b9-e20b-4cdf-80ba-43c3b9fa15d3', NULL, 0, 0, NULL, 1, 0, N'subdivision.mapping@sfdpw.org')
INSERT INTO [dbo].[AspNetUsers] ([Id], [DataId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ea1e0252-f168-4e86-9b07-dab585e2012b', 3, N'applicant@sfdpw.org', 0, N'AI7a7FZfj+1zeDs7Wo4id1CpoMtF4QIoejld8BU2RwezTMr1gdydbS2kZdXKhEkpSg==', N'32b4f8a5-24c7-45f1-894a-e16f73026cd9', NULL, 0, 0, NULL, 1, 0, N'applicant@sfdpw.org')

SET IDENTITY_INSERT [dbo].[Applicant] ON
INSERT INTO [dbo].[Applicant] ([Id], [User_Id]) VALUES (2, N'78f5cd5b-2e47-4aac-8539-6ee8879cc445')
INSERT INTO [dbo].[Applicant] ([Id], [User_Id]) VALUES (1, N'7fc32ec4-2d0b-43e1-a8c3-44f17c418fab')
INSERT INTO [dbo].[Applicant] ([Id], [User_Id]) VALUES (3, N'ea1e0252-f168-4e86-9b07-dab585e2012b')
SET IDENTITY_INSERT [dbo].[Applicant] OFF

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1c7fc6b4-28ec-4d9b-a55a-314ffd68922e', N'AdminRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'600396e5-e864-4252-bf95-ae2d2ac22ce9', N'BsmRole')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7fc32ec4-2d0b-43e1-a8c3-44f17c418fab', N'1c7fc6b4-28ec-4d9b-a55a-314ffd68922e')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'78f5cd5b-2e47-4aac-8539-6ee8879cc445', N'600396e5-e864-4252-bf95-ae2d2ac22ce9')
");
        }
        
        public override void Down()
        {
        }
    }
}
