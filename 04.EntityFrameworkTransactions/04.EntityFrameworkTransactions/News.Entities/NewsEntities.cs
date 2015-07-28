namespace News.Entities
{

    using Migrations;
    using System.Data.Entity;
    using Models;

    public class NewsEntities : DbContext
    {
        // Your context has been configured to use a 'NewsEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'News.Entities.NewsEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'NewsEntities' 
        // connection string in the application configuration file.
        public NewsEntities()
            : base("NewsEntities")
        {
            Database.SetInitializer<NewsEntities>(new MigrateDatabaseToLatestVersion<NewsEntities, Configuration>());
        }

        public virtual DbSet<News> News { get; set; }
    }
}