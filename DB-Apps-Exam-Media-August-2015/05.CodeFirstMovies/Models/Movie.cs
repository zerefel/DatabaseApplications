using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.CodeFirstMovies.Models
{
    public class Movie
    {
        private ICollection<User> users; 
        private ICollection<Rating> ratingsGiven; 

        public Movie()
        {
            this.users = new HashSet<User>();
            this.ratingsGiven = new HashSet<Rating>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }
        public AgeRestriction AgeRestriction { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<Rating> RatingsGiven
        {
            get { return this.ratingsGiven; }
            set { this.ratingsGiven = value; }
        }
    }
}