using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Represents a data model of business object, contains annotations for table fields and database relationships
    /// </summary>
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
