using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class PostCategoryViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
