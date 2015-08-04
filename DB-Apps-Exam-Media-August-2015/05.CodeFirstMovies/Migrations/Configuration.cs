using System.Threading;
using Newtonsoft.Json.Linq;
using _05.CodeFirstMovies.Models;

namespace _05.CodeFirstMovies.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_05.CodeFirstMovies.MovieEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(_05.CodeFirstMovies.MovieEntities context)
        {

            if (!context.Countries.Any())
            {
                var countriesText = File.ReadAllText("../../countries.json");
                var countriesJson = JArray.Parse(countriesText);

                foreach (var country in countriesJson)
                {
                    string countryName = country["name"].Value<string>();
                    context.Countries.Add(new Country()
                    {
                        Name = countryName
                    });
                }
            }

            context.SaveChanges();

            if (!context.Users.Any())
            {
                var usersText = File.ReadAllText("../../users.json");
                var usersJson = JArray.Parse(usersText);

                foreach (var user in usersJson)
                {
                    string username = user["username"].Value<string>();
                    int? age = user["age"].Value<int?>();
                    string countryName = user["country"].Value<string>();
                    var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
                    string email = user["email"].Value<string>();

                    var userObj = new User()
                    {
                        Username = username,
                        Age = age,
                        Country = country,
                        Email = email
                    };

                    context.Users.Add(userObj);
                }
            }

            context.SaveChanges();

            if (!context.Movies.Any())
            {
                var moviesText = File.ReadAllText("../../movies.json");
                var moviesJson = JArray.Parse(moviesText);

                foreach (var movie in moviesJson)
                {
                    string isbn = movie["isbn"].Value<string>();
                    string title = movie["title"].Value<string>();
                    int ageRestriction = movie["ageRestriction"].Value<int>();

                    var movieObj = new Movie()
                    {
                        AgeRestriction = (AgeRestriction)ageRestriction,
                        ISBN = isbn,
                        Title = title
                    };

                    context.Movies.Add(movieObj);
                }
            }

            context.SaveChanges();



            var ratingsText = File.ReadAllText("../../movie-ratings.json");
            var ratingsJson = JArray.Parse(ratingsText);

            foreach (var rating in ratingsJson)
            {
                string username = rating["user"].Value<string>();
                string movie = rating["movie"].Value<string>();
                int ratingScore = rating["rating"].Value<int>();
                var movieObj = context.Movies.FirstOrDefault(m => m.ISBN == movie);
                var userObj = context.Users.FirstOrDefault(u => u.Username == username);

                context.Ratings.Add(new Rating()
                {
                    User = userObj,
                    Movie = movieObj,
                    Stars = ratingScore
                });
            }


            context.SaveChanges();



            var favoriteMoviesText = File.ReadAllText("../../users-and-favourite-movies.json");
            var favoriteMoviesJson = JArray.Parse(favoriteMoviesText);

            foreach (var userMovies in favoriteMoviesJson)
            {
                string username = userMovies["username"].Value<string>();

                var user = context.Users.FirstOrDefault(u => u.Username == username);

                foreach (var movie in userMovies["favouriteMovies"])
                {
                    var movieName = movie.Value<string>();
                    var movieObj = context.Movies.FirstOrDefault(m => m.ISBN == movieName);

                    user.FavoriteMovies.Add(movieObj);
                }
            }

        }
    }
}