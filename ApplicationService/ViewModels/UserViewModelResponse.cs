using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class UserViewModelResponse
    {
        [Required]

        public string UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

       

        [Required]
        public IEnumerable<string> Roles { get; set; }

    }
}
