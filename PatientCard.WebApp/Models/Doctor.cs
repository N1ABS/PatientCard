using System;
using System.Collections.Generic;

namespace PatientCard.WebApp.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            VisitLog = new HashSet<VisitLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<VisitLog> VisitLog { get; set; }
    }
}
