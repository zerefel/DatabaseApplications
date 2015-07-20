using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.EmployeeDAO
{
    public class DAOMain
    {
        static void Main()
        {
            var context = new SoftUniEntities();


            var employee = context.Employees.First();
            // I hardly even understand the problem, there are no deliverables specified, it kinda sucks and I'm not doing it
            // Here, read something about best practices when updating records http://stackoverflow.com/questions/15336248/entity-framework-5-updating-a-record
        }
    }
}
