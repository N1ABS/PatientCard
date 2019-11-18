using System;
using System.Collections.Generic;

namespace PatientCard.WebApp.Models
{
    public partial class Patient
    {
        public Patient()
        {
            VisitLog = new HashSet<VisitLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Iin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<VisitLog> VisitLog { get; set; }
    }
}
