using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class EditOfficeRequestVM
    {
        [Required,GreaterThanZero]
        public int OfficeId { get; set; }
        [Required, MaxLength(100)]
        public string Location { get; set; }

        [Required,MaxLength(100)]
        public string Name { get; set; }

        public bool IsKitchen { get; set; } = false;

    }
}
