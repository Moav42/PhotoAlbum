using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CommentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime AddingDate { get; set; }
    }
}
