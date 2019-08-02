using System.Linq;
using System.Threading.Tasks;

using Model.Student;

namespace InstitutionOfHigherEducation.Data.DAL.Student
{
    public class AcademicDAL
    {
        private IHEContext _context;

        public AcademicDAL(IHEContext context)
        {
            _context = context;
        }

        public IQueryable<Academic> GetAcademicsSortedByName()
        {
            return _context.Academics.OrderBy(academic => academic.Name);
        }

        public async Task<Academic> GetAcademicById(long id)
        {
            return await _context.Academics.FindAsync(id);
        }

        public async Task<Academic> SaveAcademic(Academic academic)
        {
            if (academic.Id == null)
            {
                _context.Academics.Add(academic);
            }
            else
            {
                _context.Academics.Update(academic);
            }

            await _context.SaveChangesAsync();
            return academic;
        }

        public async Task<Academic> RemoveAcademicById(long id)
        {
            Academic academic = await GetAcademicById(id);
            _context.Academics.Remove(academic);
            await _context.SaveChangesAsync();
            return academic;
        }
    }
}