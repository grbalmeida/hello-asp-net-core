using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Model.Entries;
using InstitutionOfHigherEducation.Data;

namespace InstitutionOfHigherEducation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHEContext _context;

        public DepartmentController(IHEContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await _context.Departments
                    .Include(department => department.Institution)
                    .OrderBy(department => department.Name)
                    .ToListAsync()
            );
        }

        public IActionResult Create()
        {
            var institutions = _context.Institutions.OrderBy(institution => institution.Name).ToList();
            institutions.Insert(0, new Institution() {
                Id = 0,
                Name = "Select institution"
            });
            ViewBag.Institutions = institutions;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,InstitutionId")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Could not enter data.");
            }

            return View(department);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);

            if (departament == null)
            {
                return NotFound();
            }

            ViewBag.Institutions = new SelectList(
                _context.Institutions.OrderBy(institution => institution.Name),
                "Id",
                "Name",
                departament.InstitutionId
            );

            return View(departament);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,Name,InstitutionId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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

            ViewBag.Institutions = new SelectList(
                _context.Institutions.OrderBy(institution => institution.Name),
                "InstitutionId",
                "Name",
                department.InstitutionId
            );

            return View(department);
        }

        public bool DepartmentExists(long? id)
        {
            return _context.Departments.Any(department => department.Id == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
            _context.Institutions.Where(institution => departament.InstitutionId == institution.Id).Load();

            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
            _context.Institutions.Where(institution => department.InstitutionId == institution.Id).Load();

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departament = await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
            _context.Departments.Remove(departament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}