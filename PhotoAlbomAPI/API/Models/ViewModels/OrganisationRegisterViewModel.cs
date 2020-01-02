using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class OrganisationRegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }


    }
}
