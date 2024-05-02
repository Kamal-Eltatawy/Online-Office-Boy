using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace ApplicationService.ViewModels
{
    public class UserViewModelRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(15, MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid Egyptian mobile number.")]
        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian mobile number.")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(15, MinimumLength = 6)]
        public string UserName { get; set; }

        [Required, StringLength(15, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Office"), Required]

        public int OfficeID { get; set; }

        [Display(Name = "Paid Period"), Required]
        public int PaidPeriod { get; set; }
    }
}
