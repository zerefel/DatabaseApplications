using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _05.ConcurrentDatabaseChanges;

class ConcurrentChangesMain
{
    static void Main()
    {
        var context1 = new SoftUniEntities();
        var context2 = new SoftUniEntities();

        var emp1 = context1.Employees.Find(1);
        emp1.FirstName = "Gosho";

        var emp2 = context2.Employees.Find(1);
        emp2.FirstName = "Pesho";

        context1.SaveChanges();
        context2.SaveChanges();
    }
}