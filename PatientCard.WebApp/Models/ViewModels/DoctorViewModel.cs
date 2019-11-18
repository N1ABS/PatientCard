using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace PatientCard.WebApp.Models.ViewModels
{
    public class DoctorViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        public string SpecialtyName { get; set; }

        [Required]
        [Display(Name = "Специальность")]
        public int SpecialtyId { get; set; }
    }
}