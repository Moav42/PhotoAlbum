using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class DeleteUserViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
