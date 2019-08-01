using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using InstitutionOfHigherEducation.Models;
using InstitutionOfHigherEducation.Data;


namespace InstitutionOfHigherEducation.Controllers
{
    public class InstitutionController : Controller
    {
        private readonly IHEContext _context;

        public InstitutionController(IHEContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutions.OrderBy(institution => institution.Name).ToListAsync());
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
                    _context.Add(institution);
                    await _context.SaveChangesAsync();
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
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions.SingleOrDefaultAsync(i => i.Id == id);

            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
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
                    _context.Update(institution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionExists(institution.Id))
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

        public bool InstitutionExists(long? id)
        {
            return _context.Institutions.Any(institution => institution.Id == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .Include(i => i.Departments)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions.SingleOrDefaultAsync(i => i.Id == id);

            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var institution = await _context.Institutions.SingleOrDefaultAsync(d => d.Id == id);
            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}