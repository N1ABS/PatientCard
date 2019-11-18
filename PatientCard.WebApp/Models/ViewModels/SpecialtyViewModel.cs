using System.ComponentModel.DataAnnotations;

namespace PatientCard.WebApp.Models.ViewModels
{
    public class SpecialtyViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Специальность")]
        public string Name { get; set; }
    }
}