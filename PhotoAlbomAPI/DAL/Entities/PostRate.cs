using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class PostRate
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public bool Licked { get; set; }
    }
}
