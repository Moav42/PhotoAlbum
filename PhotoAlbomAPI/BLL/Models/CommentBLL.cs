﻿using System;

namespace BLL.Models
{
    public class CommentBLL
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime AddingDate { get; set; }
    }
}
