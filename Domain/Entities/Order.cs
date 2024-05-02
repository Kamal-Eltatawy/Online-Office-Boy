using Domain.Const;
using Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order 
    {
        public int Id { get; set; }

        [Required,GreaterThanZeroDecimal]
        [Column(TypeName = "decimal(19,3)")] 
        public decimal EmployeeTotalPrice { get; set; }
        public decimal GuestTotalPrice { get; set; }

        [Column(TypeName = "Date")] 

        public DateTime CreatedDate { get; set; }
 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } 
        public virtual User User { get; set; }

        public virtual List<OrderProducts>?  OrderProducts { get; set; } = new List<OrderProducts>();

    }
}
