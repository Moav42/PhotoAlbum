using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class EditUserNameViewModel
    {
        [Required]
        public string OldName { get; set; }
        [Required]
        public string NewName { get; set; }
    }
}
