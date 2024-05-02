using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OrderEditIsPaidRequestVM
    {
        [Required]
        public int OrderId { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public bool IsPaid { get; set; }
    }
}
