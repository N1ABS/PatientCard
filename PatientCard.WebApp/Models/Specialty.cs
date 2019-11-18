using System;
using System.Collections.Generic;

namespace PatientCard.WebApp.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
