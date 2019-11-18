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
    public class VisitLogController : Controller
    {
        private readonly PatientCardDbContext _context;

        public VisitLogController(PatientCardDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patientCardDbContext = _context.VisitLog.Include(v => v.Doctor).Include(v => v.Patient);
            PopulateDoctors();
            PopulatePatients();
            return View(await patientCardDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitLog = await _context.VisitLog
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitLog == null)
            {
                return NotFound();
            }

            return View(visitLog);
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

        private void PopulatePatients()
        {
            var patients = _context.Patient.Select(p => new PatientViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone = p.Phone,
                Iin = p.Iin
            });

            ViewData["patients"] = patients.ToList();
            ViewData["defaultPatient"] = patients.First();
        }

        public ActionResult VisitLogRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_context.VisitLog
                .Include(v => v.Doctor)
                .Include(v => v.Patient).ToList().Select(v => new VisitLogViewModel
            {
                Id = v.Id,
                PatientId = v.PatientId,
                PatientName = v.Patient.Name,
                DoctorId = v.DoctorId,
                DoctorName = v.Doctor.Name,
                Complaint = v.Complaint,
                Diagnosis = v.Diagnosis,
                VisitDate = v.VisitDate
            }).ToDataSourceResult(request));
        }

        public ActionResult VisitLogByPatientIdRead([DataSourceRequest]DataSourceRequest request, int patientId)
        {
            return Json(_context.VisitLog
                .Include(v => v.Doctor)
                .Include(v => v.Patient).ToList().Where(v => v.PatientId == patientId).Select(v => new VisitLogViewModel
                {
                    Id = v.Id,
                    PatientId = v.PatientId,
                    PatientName = v.Patient.Name,
                    DoctorId = v.DoctorId,
                    DoctorName = v.Doctor.Name,
                    Complaint = v.Complaint,
                    Diagnosis = v.Diagnosis,
                    VisitDate = v.VisitDate
                }).ToDataSourceResult(request));
        }


        [AcceptVerbs("Post")]
        public ActionResult VisitLogUpdate([DataSourceRequest] DataSourceRequest request, VisitLogViewModel visitLog)
        {
            if (visitLog != null && ModelState.IsValid)
            {
                var vl = _context.VisitLog.SingleOrDefault(dd => dd.Id == visitLog.Id);

                if (vl != null)
                {
                    vl.DoctorId = visitLog.DoctorId;
                    vl.PatientId = visitLog.PatientId;
                    vl.Complaint = visitLog.Complaint;
                    vl.Diagnosis = visitLog.Diagnosis;
                    vl.VisitDate = visitLog.VisitDate;

                    _context.VisitLog.Update(vl);
                    _context.SaveChanges();
                }

            }

            return Json(new[] { visitLog }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult VisitLogCreate([DataSourceRequest] DataSourceRequest request, VisitLogViewModel visitLog)
        {
            if (visitLog != null && ModelState.IsValid)
            {
                var vl = new VisitLog
                {
                    DoctorId = visitLog.DoctorId,
                    PatientId = visitLog.PatientId,
                    Complaint = visitLog.Complaint,
                    Diagnosis = visitLog.Diagnosis,
                    VisitDate = visitLog.VisitDate
                };

                _context.VisitLog.Add(vl);
                _context.SaveChanges();
            }

            return Json(new[] { visitLog }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public ActionResult VisitLogDelete([DataSourceRequest] DataSourceRequest request, VisitLogViewModel visitLog)
        {
            if (visitLog != null && ModelState.IsValid)
            {
                var v = _context.VisitLog.SingleOrDefault(vl => vl.Id == visitLog.Id);
                if (v != null)
                {
                    _context.VisitLog.Remove(v);
                    _context.SaveChanges();
                }
            }

            return Json(new[] { visitLog }.ToDataSourceResult(request, ModelState));
        }



        private bool VisitLogExists(int id)
        {
            return _context.VisitLog.Any(e => e.Id == id);
        }
    }
}
