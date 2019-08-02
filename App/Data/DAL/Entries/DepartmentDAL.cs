using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Model.Entries;

namespace InstitutionOfHigherEducation.Data.DAL.Entries
{
    public class DepartmentDAL
    {
        private IHEContext _context;

        public DepartmentDAL(IHEContext context)
        {
            _context = context;
        }

        public IQueryable<Department> GetDepartmentsSortedByName()
        {
            return _context.Departments
                .Include(department => department.Institution)
                .OrderBy(department => department.Name);
        }

        public async Task<Department> GetDepartmentById(long id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
            _context.Institutions
                .Where(institution => department.InstitutionId == institution.Id)
                .Load();
            return department;
        }

        public async Task<Department> SaveDepartment(Department department)
        {
            if (department.Id == null)
            {
                _context.Departments.Add(department);
            }
            else
            {
                _context.Departments.Update(department);
            }

            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> RemoveDepartmentById(long id)
        {
            Department department = await GetDepartmentById(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }   
}