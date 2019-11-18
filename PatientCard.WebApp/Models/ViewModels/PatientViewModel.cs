using System.ComponentModel.DataAnnotations;

namespace PatientCard.WebApp.Models.ViewModels
{
    public class PatientViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ИИН")]
        public string Iin { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}