using Microsoft.EntityFrameworkCore;

using InstitutionOfHigherEducation.Models;

namespace InstitutionOfHigherEducation.Data
{
    public class IHEContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public IHEContext(DbContextOptions<IHEContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().ToTable("Department");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IHECasaDoCodigo;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}