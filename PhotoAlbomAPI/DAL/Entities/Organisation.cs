using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Organisation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
