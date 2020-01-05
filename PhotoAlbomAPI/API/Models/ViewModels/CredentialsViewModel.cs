using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class CredentialsViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
