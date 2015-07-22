using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks; 

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}