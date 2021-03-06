﻿using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class PostCategories
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
