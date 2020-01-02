using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
