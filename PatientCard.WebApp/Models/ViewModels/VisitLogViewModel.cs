using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCard.WebApp.Models.ViewModels
{
    public class VisitLogViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Доктор")]
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
        
        [Required]
        [Display(Name = "Пациент")]
        public int PatientId { get; set; }

        public string PatientName { get; set; }
        
        [Required]
        [Display(Name = "Диагноз")]
        public string Diagnosis { get; set; }

        [Display(Name = "Жалобы")]
        public string Complaint { get; set; }

        [Display(Name = "Дата посещения")]
        public DateTime? VisitDate { get; set; }

    }
}