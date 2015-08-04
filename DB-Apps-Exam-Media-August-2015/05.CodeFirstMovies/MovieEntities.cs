using _05.CodeFirstMovies.Migrations;

namespace _05.CodeFirstMovies
{
    using _05.CodeFirstMovies.Models;
using System;
using System.Data.Entity;
using System.Linq;

    public class MovieEntities : DbContext
    {
        // Your context has been configured to use a 'MovieEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_05.CodeFirstMovies.MovieEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MovieEntities' 
        // connection string in the application configuration file.
        public MovieEntities()
            : base("MovieEntities")
        {
            var migration = new MigrateDatabaseToLatestVersion<MovieEntities, Configuration>();
            Database.SetInitializer(migration);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}