using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class ReportProductDetailsVM
    {
        public string ProductName { get; set; }

        public string ProductPrice { get; set; }
        public int Quantity { get; set; }

        public decimal TotalProductPrice { get; set; }
        public string OfficeBoyName { get; set; }
        public string ProductType { get; set; }

        public string Distination { get; set; }
        public string ProductDescription { get; set; }


    }
}
