using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSystem.Entities;
using StudentSystem.Models.Enums;

namespace StudentSystem.App
{
    class StudentSystemMain
    {
        static void Main()
        {
            var context = new StudentSystemEntities();


            // 01. List all students with their homeworks
            var studentsAndHomeworks = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        Content = h.Content,
                        ContentType = h.ContentType
                    })
                }).ToList();

            foreach (var student in studentsAndHomeworks)
            {
                Console.WriteLine(student.Name);

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine("----Content: {0}, Type: {1}", homework.Content,homework.ContentType);
                }
            }



            // 02.List all courses with resources
            // Note that I had no time to fill much test data so I'll just create the query here and fill test data later

            var coursesAndResources = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    Name = c.Name,
                    Description = c.Description,
                    Resources = c.Resources
                }).ToList();

            // 03. All courses with more than 5 resources

            var coursesWith5Resources = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    Name = c.Name,
                    ResourceCount = c.Resources.Count
                }).ToList();

            // 04. Courses active on a given date

            var today = DateTime.Today;

            var coursesActiveToday = context.Courses
                .Where(c => c.StartDate >= today && c.EndDate <= today)
                .Select(c => new 
                {
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    StudentsCount = c.Students.Count,
                    Duration = SqlFunctions.DateDiff("day", c.EndDate, c.StartDate)

                })
                .OrderByDescending(c => c.StudentsCount)
                .ThenByDescending(c => c.Duration)
                .ToList();

            // 05. Calculate number of courses for each student

            var studentsCourses = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    CoursesCount = s.Courses.Count,
                    TotalPrice = s.Courses.Select(c => c.Price).Sum(),
                    AverageCoursePrice = s.Courses.Select(c => c.Price).Average()
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.CoursesCount)
                .ThenBy(s => s.Name)
                .ToList();

        }
    }
}
