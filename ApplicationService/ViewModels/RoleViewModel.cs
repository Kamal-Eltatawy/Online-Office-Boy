using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string ID { get; set; }
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
