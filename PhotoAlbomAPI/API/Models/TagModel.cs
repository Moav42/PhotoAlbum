
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class TagModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
