using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Model.Entries;
using InstitutionOfHigherEducation.Data;
using InstitutionOfHigherEducation.Data.DAL.Entries;

namespace InstitutionOfHigherEducation.Controllers
{
    public class InstitutionController : Controller
    {
        private readonly IHEContext _context;
        private readonly InstitutionDAL institutionDAL;

        public InstitutionController(IHEContext context)
        {
            _context = context;
            institutionDAL = new InstitutionDAL(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(
                await institutionDAL.GetInstitutionsSortedByName()
                    .ToListAsync()
            );
        }

        private async Task<IActionResult> GetInstitutionViewById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await institutionDAL.GetInstitutionById((long) id);

            if (institution == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return View(institution);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address")]Institution institution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await institutionDAL.SaveInstitution(institution);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Could not enter data.");
            }

            return View(institution);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            return await GetInstitutionViewById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,Name,Address")] Institution institution)
        {
            if (id != institution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await institutionDAL.SaveInstitution(institution);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstitutionExists(institution.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(institution);
        }

        public async Task<bool> InstitutionExists(long? id)
        {
            return await institutionDAL.GetInstitutionById((long) id) != null;
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetInstitutionViewById(id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await GetInstitutionViewById(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var institution = await institutionDAL.RemoveInstitutionById((long) id);
            return RedirectToAction(nameof(Index));
        }
    }
}