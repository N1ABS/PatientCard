using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientCard.WebApp.Models;
using PatientCard.WebApp.Models.ViewModels;

namespace PatientCard.WebApp.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly PatientCardDbContext _context;

        public SpecialtyController(PatientCardDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialty.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        public ActionResult SpecialtyRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_context.Specialty.ToList().Select(s => new SpecialtyViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult SpecialtyUpdate([DataSourceRequest] DataSourceRequest request, SpecialtyViewModel specialty)
        {
            if (specialty != null && ModelState.IsValid)
            {
                var s = _context.Specialty.SingleOrDefault(ss => ss.Id == specialty.Id);

                if (s != null)
                {
                    s.Name = specialty.Name;

                    _context.Specialty.Update(s);
                    _context.SaveChanges();
                }

            }

            return Json(new[] { specialty }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult SpecialtyCreate([DataSourceRequest] DataSourceRequest request, SpecialtyViewModel specialty)
        {
            if (specialty != null && ModelState.IsValid)
            {
                var s = new Specialty
                {
                    Name = specialty.Name
                };

                _context.Specialty.Add(s);
                _context.SaveChanges();
            }

            return Json(new[] { specialty }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult SpecialtyDelete([DataSourceRequest] DataSourceRequest request, SpecialtyViewModel specialty)
        {
            if (specialty != null && ModelState.IsValid)
            {
                var s = _context.Specialty.SingleOrDefault(ss => ss.Id == specialty.Id);
                if (s != null)
                {
                    _context.Specialty.Remove(s);
                    _context.SaveChanges();
                }
            }

            return Json(new[] { specialty }.ToDataSourceResult(request, ModelState));
        }

        private bool SpecialtyExists(int id)
        {
            return _context.Specialty.Any(e => e.Id == id);
        }
    }
}
