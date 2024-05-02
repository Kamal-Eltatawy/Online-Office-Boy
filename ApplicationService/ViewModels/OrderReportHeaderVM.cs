using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OrderReportHeaderVM
    {
        public int OrderId { get; set; }

        public decimal EmployeeTotalPrice { get; set; }

        public decimal GuestTotalPrice { get; set; }

        public string CreatedName { get; set; }

        public string CreatedDate { get; set;}

    }
}
