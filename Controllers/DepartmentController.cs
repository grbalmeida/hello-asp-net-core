using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View(await _context.Departments.OrderBy(departament => departament.Name).ToListAsync());
        }
    }
}