using Domain.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OrderProductsResponseOrderVM
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductImgUrl { get; set; }

        public TimeSpan ReadyTime { get; set; }



        public string ProductPrice { get; set; }

        public int Quantity { get; set; }


        public bool IsPaid { get; set; }

        public bool IsGuest { get; set; }

        public OrderStatus Status { get; set; } 

        public string OfficeBoyName { get; set; }

        public string DepartmentName { get; set; }
    }
}
