using Domain.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderProducts
    {
        public int Quantity { get; set; } = 1;
        [Required]
        public int Status { get; set; }



        public bool IsPaid { get; set; }

        public bool IsGuest { get; set; }

        [ForeignKey(nameof(OfficeBoy))]

        public string? OfficeBoyId { get; set; }
        public virtual User OfficeBoy { get; set; }

        [ForeignKey(nameof(Department))]

        public int DestiniationDepartmentId { get; set; }
        public virtual Office Department { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


    }
}
