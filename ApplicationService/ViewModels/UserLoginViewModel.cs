using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        public bool KeppMe { get; set;}
    }
}
