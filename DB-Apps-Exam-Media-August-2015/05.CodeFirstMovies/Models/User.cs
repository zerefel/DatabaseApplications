using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirstMovies.Models
{
    public class User
    {
        private ICollection<Movie> favoriteMovies;
        private ICollection<Movie> ratingsGiven;

        public User()
        {
            this.favoriteMovies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Username { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Movie> FavoriteMovies
        {
            get { return this.favoriteMovies; }
            set { this.favoriteMovies = value; }
        }
    }
}
