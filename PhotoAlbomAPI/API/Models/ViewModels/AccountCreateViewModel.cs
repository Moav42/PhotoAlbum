using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class AccountCreateViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
