
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PostRateModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int PostId { get; set; }
        public bool Licked { get; set; }
    }
}
