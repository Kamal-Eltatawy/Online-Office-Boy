using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OfficeCartVM
    {
        [Required,GreaterThanZero]
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }

    }
}
