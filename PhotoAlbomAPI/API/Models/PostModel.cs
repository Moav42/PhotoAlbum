using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PostModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LocationPath { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime AddingDate { get; set; }       

    }
}
