using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    class PostTags
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Key]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
