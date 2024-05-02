using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OrderProductViewModel
    {
        [Required]
      public int ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }


        [Required]

        public bool IsGuest { get; set; }

        public int OfficeId { get; set; }

        public string OfficeBoyId { get; set; }

        public int DepartmentId { get; set; }
    }
}
