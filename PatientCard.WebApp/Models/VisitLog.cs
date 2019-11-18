using System;
using System.Collections.Generic;

namespace PatientCard.WebApp.Models
{
    public partial class VisitLog
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Complaint { get; set; }
        public DateTime? VisitDate { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
