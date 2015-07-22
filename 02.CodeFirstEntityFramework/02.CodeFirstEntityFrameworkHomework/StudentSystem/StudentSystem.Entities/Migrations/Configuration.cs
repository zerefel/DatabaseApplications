using System.Collections.Generic;
using StudentSystem.Models;
using StudentSystem.Models.Enums;

namespace StudentSystem.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Entities.StudentSystemEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystemEntities";
        }

        protected override void Seed(StudentSystem.Entities.StudentSystemEntities context)
        {
            var homework = new Homework()
            {
                AuthorId = 1,
                Content = "Some random content for that homework",
                ContentType = ContentType.Text,
                SubmissionDate = DateTime.Now
            };


            var course = new Course()
            {
                Description = "Database applications course.",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Homeworks = new List<Homework>() { homework },
                Name = "Databases",
                Price = 2352.51m

            };

            var student1 = new Student() {
                Name = "Ivan Georgiev",
                RegistrationDate = DateTime.Now,
                PhoneNumber = "521o5128951",
                Courses = new List<Course>() {course},
                Homeworks = new List<Homework>() {homework}
            };

            var student2 = new Student()
            {
                Name = "Mincho Gadjov",
                RegistrationDate = DateTime.Now,
                PhoneNumber = "27254742752",
                Courses = new List<Course>() {course}
            };

            var student3 = new Student()
            {
                Name = "Tosho Toshkov",
                RegistrationDate = DateTime.Now,
                PhoneNumber = "523525151153",
                Courses = new List<Course>() {course}
            };

            // TODO: Make the AddOrUpdate work in this case to seed only once.

            //context.Students.AddOrUpdate(student1);
            //context.Students.AddOrUpdate(student3);
            //context.Students.AddOrUpdate(student2);
        }
    }
}