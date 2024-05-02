using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class CancelOrderProductRequestVM
    {
        [Required,GreaterThanZero]
        public int ProductId { get; set; }
        [Required, GreaterThanZero]

        public int OrderId { get; set; }
    }
}
