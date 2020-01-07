using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents a data model of business object, contains annotations for table fields and database relationships
    /// </summary>
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public List<PostTags> PostTags { get; set; }
    }
}
