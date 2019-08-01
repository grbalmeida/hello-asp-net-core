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

            var institutions = new Institution[]
            {
                new Institution { Name = "UniParaná", Address = "Paraná" },
                new Institution { Name = "UniAcre", Address = "Acre" }
            };

            foreach (Institution institution in institutions)
            {
                context.Institutions.Add(institution);
            }

            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Computer Science", InstitutionId = 1 },
                new Department { Name = "Food Science", InstitutionId = 2 }
            };

            foreach (Department department in departments)
            {
                context.Departments.Add(department);
            }

            context.SaveChanges();
        }
    }
}