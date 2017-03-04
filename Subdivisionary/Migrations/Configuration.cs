using Subdivisionary.Models;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            /*
            var ecp = new FeeScheuleItem("CC")
            {
                BaseMapReviewFee = 250,
                BaseProcessingFee = 10278,
                FinalMapReviewFee = 10406,
                FinalMapPerUnitFee = 50
            };
            var nc = new FeeScheuleItem("NC")
            {
                BaseMapReviewFee = 250,
                BaseProcessingFee = 9475,
                FinalMapReviewFee = 10406,
                FinalMapPerUnitFee = 50
            };
            var lotSubd = new FeeScheuleItem("LS/LM")
            {
                BaseMapReviewFee = 250,
                BaseProcessingFee = 10278,
                FinalMapReviewFee = 10406,
                FinalMapPerUnitFee = 50
            };
            context.FeeSchedule.Add(ecp);
            context.FeeSchedule.Add(nc);
            context.FeeSchedule.Add(lotSubd);
            context.FeeSchedule.Add(new FeeScheuleItem("LLA") {BaseProcessingFee = 3416});
            context.FeeSchedule.Add(new FeeScheuleItem("AM") {BaseProcessingFee = 3416});
            context.FeeSchedule.Add(new FeeScheuleItem("CoC") {BaseProcessingFee = 2701});
            context.FeeSchedule.Add(new FeeScheuleItem("Sidewalk Legislation") {BaseProcessingFee = 0,BaseProcessingPerUnitFee = 2580});
            context.FeeSchedule.Add(new FeeScheuleItem("ROS") {BaseProcessingFee = 640});
            context.FeeSchedule.Add(new FeeScheuleItem("CR") {BaseProcessingFee = 25});
            context.SaveChanges();*/
        }
    }
}
