using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class UserWithRolesRequestVM
    {
        [Required]
        public string Id { get; set; }
     
        public string Name { get; set; }

        [Required]
        public List<string> Roles { get; set; } = new List<string>();


    }
}
