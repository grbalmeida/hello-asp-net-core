using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Model.Entries;

namespace InstitutionOfHigherEducation.Data.DAL.Entries
{
    public class InstitutionDAL
    {
        private IHEContext _context;

        public InstitutionDAL(IHEContext context)
        {
            _context = context;
        }

        public IQueryable<Institution> GetInstitutionsSortedByName()
        {
            return _context.Institutions.OrderBy(institution => institution.Name);
        }

        public async Task<Institution> GetInstitutionById(long id)
        {
            return await _context.Institutions
                .Include(institution => institution.Departments)
                .SingleOrDefaultAsync(institution => institution.Id == id);
        }

        public async Task<Institution> SaveInstitution(Institution institution)
        {
            if (institution.Id == null)
            {
                _context.Institutions.Add(institution);
            }
            else
            {
                _context.Update(institution);
            }

            await _context.SaveChangesAsync();
            return institution;
        }

        public async Task<Institution> RemoveInstitutionById(long id)
        {
            Institution institution = await GetInstitutionById(id);
            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
            return institution;
        }
    }
}