using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents a data model of business object, contains annotations for table fields and database relationships
    /// </summary>
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LocationPath { get; set; }
        public string UserId { get; set; }
        public DateTime AddingDate { get; set; }
        public User User { get; set; }
        public List<PostCategories> PostCategories { get; set; }
        public List<PostTags> PostTags { get; set; }
        public List<PostRate> PostRates { get; set; }

    }
}
