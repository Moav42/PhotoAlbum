using System.ComponentModel.DataAnnotations;

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
