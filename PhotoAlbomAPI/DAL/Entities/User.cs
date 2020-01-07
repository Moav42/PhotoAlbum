using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    /// <summary>
    /// Represents an application user model, contains annotations for table fields and database relationships, inhereted form IdentityUser
    /// </summary>
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostRate> PostRates { get; set; }
    }
}
