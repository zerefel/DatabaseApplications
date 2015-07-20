using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _06.StoredProcedure;


class StoredProcedureMain
{
    static void Main()
    {
        var context = new SoftUniEntities();

        var projects = context.usp_FindProjectsForEmployee("Ruth", "Ellerbrock").ToList();

        foreach (var project in projects)
        {
            Console.WriteLine("{0} - {1} {2}", project.Name, Regex.Replace(project.Description, "(?<=^.{30}).*", "..."), project.StartDate);
        }
    }
}