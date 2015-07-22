using StudentSystem.Entities.Migrations;

namespace StudentSystem.Entities
{
    using System.Data.Entity;
    using StudentSystem.Models;

    public class StudentSystemEntities : DbContext
    {
        public StudentSystemEntities()
            : base("StudentSystemEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemEntities, Configuration>());
            // Configure to destroy and re-create db on model changes
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentSystemEntities>());
        }

        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<License> Licenses { get; set; } 
    }
}