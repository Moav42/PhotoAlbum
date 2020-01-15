using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class PostTags
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Key]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
