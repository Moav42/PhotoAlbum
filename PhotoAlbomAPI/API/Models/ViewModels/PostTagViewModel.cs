using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class PostTagViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
}
