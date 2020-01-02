using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class EditUserNameViewModel
    {
        [Required]
        public string OldName { get; set; }
        [Required]
        public string NewName { get; set; }
    }
}
