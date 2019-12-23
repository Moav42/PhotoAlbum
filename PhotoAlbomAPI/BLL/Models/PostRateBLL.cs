using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.Models
{
    public class PostRateBLL
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public bool Licked { get; set; }
    }
}
