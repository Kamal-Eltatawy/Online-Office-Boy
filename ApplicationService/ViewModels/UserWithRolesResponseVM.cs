using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class UserWithRolesResponseVM
    {
        [Required]
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string PaidPeriod { get; set; }

        public string OfficeLocation { get; set; }

        public List<string> Roles { get; set; }
    }
}
