using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class UserRolesViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public List<CheckBoxViewModel> Roles { get; set; }
    }
}
