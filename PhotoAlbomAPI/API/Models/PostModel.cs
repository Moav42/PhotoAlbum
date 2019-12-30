using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LocationPath { get; set; }
        public string UserId { get; set; }
        public DateTime AddingDate { get; set; }
       

    }
}
