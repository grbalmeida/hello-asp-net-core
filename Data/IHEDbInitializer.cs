using System.Linq;

using InstitutionOfHigherEducation.Models;

namespace InstitutionOfHigherEducation.Data
{
    public class IHEDbInitializer
    {
        public static void Initialize(IHEContext context)
        {
            context.Database.EnsureCreated();

            if (context.Departments.Any())
            {
                return;
            }

            var departments = new Department[]
            {
                new Department { Name = "Computer Science" },
                new Department { Name = "Food Science" }
            };

            foreach (Department department in departments)
            {
                context.Departments.Add(department);
            }

            context.SaveChanges();
        }
    }
}