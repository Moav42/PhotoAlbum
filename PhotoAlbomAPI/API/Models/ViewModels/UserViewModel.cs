using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
