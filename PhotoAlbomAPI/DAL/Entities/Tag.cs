using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public List<PostTags> PostTags { get; set; }
    }
}
