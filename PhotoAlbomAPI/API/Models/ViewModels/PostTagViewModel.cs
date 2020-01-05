using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class PostTagViewModel
    {
        [Required]
        public string PostId { get; set; }
        [Required]
        public string TagId { get; set; }
    }
}
