using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class PermissionsCreateViewModel
    {
        [Required]
        public string RoleId { get; set; }
        [Required]
        public List<CheckBoxViewModel> RoleCalims { get; set; }
    }
}
