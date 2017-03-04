namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedFeeSchedule : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[FeeScheuleItem] ON
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (2, N'CC', 10278, 250, 0, 10406, 50, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (3, N'NC', 9475, 250, 0, 10406, 50, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (4, N'LS/LM', 10278, 250, 0, 10406, 50, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (5, N'LLA', 3416, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (6, N'AM', 3416, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (7, N'CoC', 2701, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (8, N'Sidewalk Legislation', 0, 0, 0, 0, 0, 2580, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (9, N'ROS', 640, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[FeeScheuleItem] ([Id], [ApplicationTypeName], [BaseProcessingFee], [BaseMapReviewFee], [BaseMapPerUnitFee], [FinalMapReviewFee], [FinalMapPerUnitFee], [BaseProcessingPerUnitFee], [AdditionalFeesMayApplyDisclaimer]) VALUES (10, N'CR', 25, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[FeeScheuleItem] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}
