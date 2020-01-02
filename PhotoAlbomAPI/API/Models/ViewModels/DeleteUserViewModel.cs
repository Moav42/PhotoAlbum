using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class DeleteUserViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
