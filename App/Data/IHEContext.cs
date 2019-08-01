using Microsoft.EntityFrameworkCore;

using Model.Entries;

namespace InstitutionOfHigherEducation.Data
{
    public class IHEContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public IHEContext(DbContextOptions<IHEContext> options): base(options)
        {

        }
    }
}