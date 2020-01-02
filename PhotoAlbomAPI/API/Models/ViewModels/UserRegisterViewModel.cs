using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
       
    }
}
