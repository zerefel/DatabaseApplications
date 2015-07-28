namespace News.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<NewsEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "News";
        }

        protected override void Seed(NewsEntities context)
        {
            //context.News.AddOrUpdate(
            //    n => n.Content, 
            //    new Models.News() { Content = "Test news 4!" },
            //    new Models.News() { Content = "Something very interesting happened!" },
            //    new Models.News() { Content = "Software University is moving to a new building!"},
            //    new Models.News() { Content = "Putin on war with the USA!"},
            //    new Models.News() { Content = "Barack Obama is dead! Long live Osama!" });


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
            //
        }
    }
}
