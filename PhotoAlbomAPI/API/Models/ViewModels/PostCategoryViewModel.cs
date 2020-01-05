using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class PostCategoryViewModel
    {
        [Required]
        public string PostId { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}
