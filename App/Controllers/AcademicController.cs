using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using InstitutionOfHigherEducation.Data;
using InstitutionOfHigherEducation.Data.DAL.Student;
using Model.Student;

namespace InstitutionOfHigherEducation.Controllers
{
    public class AcademicController : Controller
    {
        private readonly IHEContext _context;
        private readonly AcademicDAL academicDAL;

        public AcademicController(IHEContext context)
        {
            _context = context;
            academicDAL = new AcademicDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await academicDAL.GetAcademicsSortedByName().ToListAsync()
            );
        }

        private async Task<IActionResult> GetAcademicViewById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academic = await academicDAL.GetAcademicById((long) id);

            if (academic == null)
            {
                return NotFound();
            }

            return View(academic);
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetAcademicViewById(id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await GetAcademicViewById(id);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AcademicRecord,DateOfBirth")] Academic academic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await academicDAL.SaveAcademic(academic);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Could not enter data");
            }

            return View(academic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,Name,AcademicRecord,DateOfBirth")] Academic academic)
        {
            if (id != academic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await academicDAL.SaveAcademic(academic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicExists(academic.Id))
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

            return View(academic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var academic = await academicDAL.RemoveAcademicById((long) id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AcademicExists(long? id)
        {
            return await academicDAL.GetAcademicById((long) id) != null;
        }
    }
}