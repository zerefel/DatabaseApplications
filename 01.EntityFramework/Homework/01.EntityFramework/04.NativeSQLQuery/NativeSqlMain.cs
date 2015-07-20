using System;
using System.Diagnostics;
using System.Linq;
using _04.NativeSQLQuery;

class NativeSqlMain
{
    static void Main()
    {
        var context = new SoftUniEntities();
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var employeesWithSaidProjects = context.Employees
            .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
            .Select(e => e.FirstName).ToList();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);



        stopwatch.Reset();


        stopwatch.Start();
        var SQLemployeesWithSaidProjects = context.Database.SqlQuery<string>("SELECT FirstName FROM Employees AS a " +
                                                                             "INNER JOIN EmployeesProjects AS ep " +
                                                                             "ON a.EmployeeID = ep.EmployeeID " +
                                                                             "INNER JOIN Projects AS p " +
                                                                             "ON p.ProjectID = ep.ProjectID " +
                                                                             "WHERE YEAR(p.StartDate) = 2002").ToList();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
      
    }
}