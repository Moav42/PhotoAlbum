using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class EditOrganisationViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string OrgName { get; set; }
        [Required]
        public string Loacation { get; set; }

    }
}
