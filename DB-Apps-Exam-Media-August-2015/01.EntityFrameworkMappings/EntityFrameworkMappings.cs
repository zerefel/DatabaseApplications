namespace _01.EntityFrameworkMappings
{
    using System;
    using System.Linq;
    
    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var characterNames = context.Characters.Select(c => c.Name);

            foreach (var name in characterNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
