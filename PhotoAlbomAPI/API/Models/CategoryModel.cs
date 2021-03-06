﻿
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CategoryModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
