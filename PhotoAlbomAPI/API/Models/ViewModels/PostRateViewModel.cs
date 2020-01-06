using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class PostRateViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
