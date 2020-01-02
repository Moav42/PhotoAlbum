
namespace API.Models
{
    public class PostRateModel
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public bool Licked { get; set; }
    }
}
