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
    public class PatientController : Controller
    {
        private readonly PatientCardDbContext _context;

        public PatientController(PatientCardDbContext context)
        {
            _context = context;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patient.ToListAsync());
        }


        private void PopulateDoctors()
        {
            var doctors = _context.Doctor.Select(s => new DoctorViewModel
            {
                Id = s.Id,
                Name = s.Name
            });

            ViewData["doctors"] = doctors.ToList();
            ViewData["defaultDoctor"] = doctors.First();
        }

        public IActionResult PatientRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_context.Patient.ToList().Select(patient => new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Iin = patient.Iin,
                Address = patient.Address,
                Phone = patient.Phone
            }).ToDataSourceResult(request));
        }

        public async Task<IActionResult> Details(int? id)
        {
            PopulateDoctors();
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.Include(p => p.VisitLog)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (patient == null)
            {
                return NotFound();
            }


            var patientModel = new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Address = patient.Address,
                Phone = patient.Phone,
                Iin = patient.Iin
            };


            return View(patientModel);
        }
        

        [AcceptVerbs("Post")]
        public ActionResult PatientUpdate([DataSourceRequest] DataSourceRequest request, PatientViewModel patient)
        {
            if (patient != null && ModelState.IsValid)
            {
                var p1 = _context.Patient.SingleOrDefault(pp => pp.Id == patient.Id);

                if (p1 != null)
                {
                    p1.Name = patient.Name;
                    p1.Iin = patient.Iin;
                    p1.Address = patient.Address;
                    p1.Phone = patient.Phone;


                    _context.Patient.Update(p1);
                    _context.SaveChanges();
                }

            }

            return Json(new[] { patient }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult PatientCreate([DataSourceRequest] DataSourceRequest request, PatientViewModel patient)
        {
            if (patient != null && ModelState.IsValid)
            {
                var p1 = new Patient
                {
                    Name = patient.Name,
                    Iin = patient.Iin,
                    Address = patient.Address,
                    Phone = patient.Phone
                };

                _context.Patient.Add(p1);
                _context.SaveChanges();
            }

            return Json(new[] { patient }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult PatientDelete([DataSourceRequest] DataSourceRequest request, PatientViewModel patient)
        {
            if (patient != null && ModelState.IsValid)
            {
                var p1 = _context.Patient.SingleOrDefault(p => p.Id == patient.Id);
                if (p1 != null)
                {
                    _context.Patient.Remove(p1);
                    _context.SaveChanges();
                }
            }

            return Json(new[] { patient }.ToDataSourceResult(request, ModelState));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
