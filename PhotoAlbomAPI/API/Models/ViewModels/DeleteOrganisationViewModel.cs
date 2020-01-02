using System.ComponentModel.DataAnnotations;

namespace API.Models.ViewModels
{
    public class DeleteOrganisationViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int OrgId { get; set; }
    }
}
