using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InstitutionOfHigherEducation.Models;

namespace InstitutionOfHigherEducation.Controllers
{
    public class InstitutionController : Controller
    {
        private static IList<Institution> institutions = new List<Institution>()
        {
            new Institution() {
                Id = 1,
                Name = "UniParaná",
                Address = "Paraná"
            },
            new Institution() {
                Id = 2,
                Name = "UniSanta",
                Address = "Santa Catarina"
            },
            new Institution() {
                Id = 3,
                Name = "UniSãoPaulo",
                Address = "São Paulo"
            },
            new Institution() {
                Id = 4,
                Name = "UniSulgrandense",
                Address = "Rio Grande do Sul"
            },
            new Institution() {
                Id = 5,
                Name = "UniCarioca",
                Address = "Rio de Janeiro"
            }
        };
        public IActionResult Index()
        {
            return View(institutions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Institution institution)
        {
            institutions.Add(institution);
            institution.Id = institutions.Select(i => i.Id).Max() + 1;

            return RedirectToAction("Index");
        }
    }
}