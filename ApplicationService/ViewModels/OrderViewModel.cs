using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public decimal EmployeeTotalPrice { get; set; }
        [Required]
        public decimal GuestTotalPrice { get; set; }

        public List<OrderProductViewModel> OrderProductsData { get; set; }


    }
}
