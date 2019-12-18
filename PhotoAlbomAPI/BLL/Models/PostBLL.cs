using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.Models
{
    public class PostBLL
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LocationPath { get; set; }
        public string UserId { get; set; }
        public DateTime AddingDate { get; set; }

    }
}
