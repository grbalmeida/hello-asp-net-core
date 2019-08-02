using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Model.Entries;
using InstitutionOfHigherEducation.Data;
using InstitutionOfHigherEducation.Data.DAL.Entries;

namespace InstitutionOfHigherEducation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHEContext _context;
        private readonly DepartmentDAL departmentDAL;
        private readonly InstitutionDAL institutionDAL;

        public DepartmentController(IHEContext context)
        {
            this._context = context;
            institutionDAL = new InstitutionDAL(context);
            departmentDAL = new DepartmentDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await departmentDAL.GetDepartmentsSortedByName().ToListAsync()
            );
        }

        private async Task<IActionResult> GetDepartmentViewById(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await departmentDAL.GetDepartmentById((long) id);

            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        public IActionResult Create()
        {
            var institutions = institutionDAL.GetInstitutionsSortedByName().ToList();
            institutions.Insert(0, new Institution() { Id = 0, Name = "Select institution" });
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
                    await departmentDAL.SaveDepartment(department);
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
            ViewResult departmentView = (ViewResult) await GetDepartmentViewById(id);
            Department department = (Department) departmentView.Model;

            ViewBag.Institutions = new SelectList(
                _context.Institutions.OrderBy(institution => institution.Name),
                "Id",
                "Name",
                department.InstitutionId
            );

            return departmentView;
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
                    await departmentDAL.SaveDepartment(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(department.Id))
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

        private async Task<bool> DepartmentExists(long? id)
        {
            return await departmentDAL.GetDepartmentById((long) id) != null;
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetDepartmentViewById(id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await GetDepartmentViewById(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departament = await departmentDAL.RemoveDepartmentById((long) id);
            return RedirectToAction(nameof(Index));
        }
    }
}