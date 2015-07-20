using System;
using System.Linq;

using _03.DatabaseSearchQuries;
public class SearchQueriesMain
{
    static void Main()
    {
        var context = new SoftUniEntities();

        // 01. All employees with projects with start date between 2001 and 2003

        var employeesByProjectDate = context.Employees
            .Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
            .Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                ManagerName = emp.Manager.FirstName,
                Projects = emp.Projects.Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
            }).ToList();


        foreach (var emp in employeesByProjectDate)
        {
            Console.WriteLine("FirstName: {0}, LastName: {1}, ManagerName: {2}", emp.FirstName, emp.LastName, emp.ManagerName);

            if (!emp.Projects.Any())
            {
                continue;
            }

            Console.WriteLine("Projects:");

            foreach (var project in emp.Projects)
            {
                Console.WriteLine("Name: {0}, StartDate: {1}, EndDate: {2}", project.Name, project.StartDate, project.EndDate);
            }

            Console.WriteLine();
        }




        // 02. All addresses ordered by number of employees

        var addressesByEmployeeCount = context.Addresses.OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town.Name).Take(10)
            .Select(a => new
            {
                AddressText = a.AddressText,
                TownName = a.Town.Name,
                EmployeesCount = a.Employees.Count
            }).ToList();

        foreach (var address in addressesByEmployeeCount)
        {
            Console.WriteLine("{0}, {1}, {2} employees", address.AddressText, address.TownName, address.EmployeesCount);
        }





        // 03. Employee by ID

        var employee = context.Employees
            .Where(emp => emp.EmployeeID == 120)
            .Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                JobTitle = emp.JobTitle,
                Projects = emp.Projects.OrderBy(p => p.Name).Select(p => p.Name)
            }).First();

        Console.WriteLine("Name: {0} {1}, Job Title: {2}", employee.FirstName, employee.LastName, employee.JobTitle);
        Console.WriteLine("Projects:");
        foreach (var p in employee.Projects)
        {
            Console.WriteLine(p);
        }





        // 04. All Departments with more than 5 employees

        var departmentsWith5Employees = context.Departments.Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .Select(d => new
            {
                DepartmentName = d.Name,
                ManagerName = d.Manager.FirstName,
                Employees = d.Employees.Where(emp => emp.DepartmentID == d.DepartmentID).Select(emp => new
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    HireDate = emp.HireDate,
                    JobTitle = emp.JobTitle
                })
            }).ToList();

        Console.WriteLine(departmentsWith5Employees.Count);

        foreach (var dept in departmentsWith5Employees)
        {
            Console.WriteLine("--{0} - Manager: {1}, Employees: {2}", dept.DepartmentName, dept.ManagerName, dept.Employees.Count());
        }

    }
}