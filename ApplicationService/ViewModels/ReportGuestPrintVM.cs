using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class ReportGuestPrintVM
    {
        public int OrderId { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedName { get; set; }


        public string BillTo { get; set; } = "Company";

        public decimal TotalPrice { get; set; }

     public    List<ReportProductDetailsVM> ProductDetails { get; set; } = new List<ReportProductDetailsVM>();
    }
}
