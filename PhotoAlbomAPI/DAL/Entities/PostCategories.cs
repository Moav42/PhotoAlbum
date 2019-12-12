using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    class PostCategories
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
