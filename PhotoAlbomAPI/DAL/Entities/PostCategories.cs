using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents a data model of business object, contains annotations for table fields and database relationships
    /// </summary>
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
