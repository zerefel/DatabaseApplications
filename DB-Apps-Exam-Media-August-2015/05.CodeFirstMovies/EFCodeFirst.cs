using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using _05.CodeFirstMovies.Models;

namespace _05.CodeFirstMovies
{
    class EFCodeFirst
    {
        static void Main()
        {
            var context = new MovieEntities();

            Console.WriteLine(context.Countries.Any());

            // 01. Export adult movies

            var adultMovies = context.Movies
                .OrderBy(m => m.Title)
                .ThenBy(m => m.RatingsGiven.Count)
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.RatingsGiven.Count
                });

            var json = new JavaScriptSerializer().Serialize(adultMovies);
            File.WriteAllText("../../adult-movies.json", json);

            // 02. Rated movies by user
           

        }
    }
}
