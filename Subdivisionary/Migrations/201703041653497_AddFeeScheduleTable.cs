namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeeScheduleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeeScheuleItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationTypeName = c.String(),
                        BaseProcessingFee = c.Single(nullable: false),
                        BaseMapReviewFee = c.Single(nullable: false),
                        BaseMapPerUnitFee = c.Single(nullable: false),
                        FinalMapReviewFee = c.Single(nullable: false),
                        FinalMapPerUnitFee = c.Single(nullable: false),
                        BaseProcessingPerUnitFee = c.Single(nullable: false),
                        AdditionalFeesMayApplyDisclaimer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeeScheuleItem");
        }
    }
}
