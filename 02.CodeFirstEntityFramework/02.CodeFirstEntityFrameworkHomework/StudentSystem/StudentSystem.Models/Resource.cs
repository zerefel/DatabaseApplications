using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    using StudentSystem.Models.Enums;

    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ResourceType ResourceType { get; set; }
        public string URL { get; set; }

        public ICollection<License> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }
    }
}