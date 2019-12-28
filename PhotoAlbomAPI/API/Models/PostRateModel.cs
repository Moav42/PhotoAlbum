using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PostRateModel
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public bool Licked { get; set; }
    }
}
