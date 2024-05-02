using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class UserUpdateViewModel
    {
        [Required]

        public string UserID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        public IEnumerable<string> RolesID { get; set; }
    }
}
