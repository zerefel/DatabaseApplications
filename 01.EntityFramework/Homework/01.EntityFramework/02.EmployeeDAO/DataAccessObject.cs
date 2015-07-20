using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.EmployeeDAO
{
    public static class DataAccessObject
    {
        public static void Add(Employee emp)
        {
            using (var context = new SoftUniEntities())
            {
                context.Employees.Add(emp);
                context.SaveChanges();
            }
        }

        public static Employee FindByKey(object key)
        {
            using(var context = new SoftUniEntities())
            {
                return context.Employees.Find(key);
            }
        }

        public static void Modify(Employee emp)
        {
           // No idea as to what to modify, this problem is very poorly described IMO
        }

        public static void Delete(Employee emp)
        {
            using (var context = new SoftUniEntities())
            {
                context.Employees.Attach(emp);
                context.Employees.Remove(emp);

                context.SaveChanges();
            }
        }
    }
}
