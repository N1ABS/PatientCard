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
    public class DoctorController : Controller
    {
        private readonly PatientCardDbContext _context;

        public DoctorController(PatientCardDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patientCardDbContext = _context.Doctor.Include(d => d.Specialty);
            PopulateSpecialties();
            return View(await patientCardDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        private void PopulateSpecialties()
        {
            var specialties = _context.Specialty.Select(s => new SpecialtyViewModel
            {
                Id = s.Id,
                Name = s.Name
            });

            ViewData["specialties"] = specialties.ToList();
            ViewData["defaultSpecialty"] = specialties.First();
        }


        public ActionResult DoctorRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_context.Doctor.Include(d => d.Specialty).ToList().Select(d => new DoctorViewModel
            {
                Id = d.Id,
                Name = d.Name,
                SpecialtyId = d.SpecialtyId,
                SpecialtyName = d.Specialty.Name
            }).ToDataSourceResult(request));
        }


        [AcceptVerbs("Post")]
        public ActionResult DoctorUpdate([DataSourceRequest] DataSourceRequest request, DoctorViewModel doctor)
        {
            if (doctor != null && ModelState.IsValid)
            {
                var d = _context.Doctor.SingleOrDefault(dd => dd.Id == doctor.Id);

                if (d != null)
                {
                    d.Name = doctor.Name;
                    d.SpecialtyId = doctor.SpecialtyId;

                    _context.Doctor.Update(d);
                    _context.SaveChanges();
                }

            }

            return Json(new[] { doctor }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult DoctorCreate([DataSourceRequest] DataSourceRequest request, DoctorViewModel doctor)
        {
            if (doctor != null && ModelState.IsValid)
            {
                var d = new Doctor
                {
                    Name = doctor.Name,
                    SpecialtyId = doctor.SpecialtyId
                };

                _context.Doctor.Add(d);
                _context.SaveChanges();
            }

            return Json(new[] { doctor }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult DoctorDelete([DataSourceRequest] DataSourceRequest request, DoctorViewModel doctor)
        {
            if (doctor != null && ModelState.IsValid)
            {
                var d = _context.Doctor.SingleOrDefault(dd => dd.Id == doctor.Id);
                if (d != null)
                {
                    _context.Doctor.Remove(d);
                    _context.SaveChanges();
                }
            }

            return Json(new[] { doctor }.ToDataSourceResult(request, ModelState));
        }
        

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
