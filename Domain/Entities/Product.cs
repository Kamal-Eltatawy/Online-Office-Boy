using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]

        public string Name { get; set; }
        [Required, MaxLength(100)]

        public string Description { get; set; }

        [Required, GreaterThanZeroDecimal]
        [Column(TypeName = "decimal(19,3)")]

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan ReadyTime { get; set; }
        public string PictureUrl { get; set; }
        [ForeignKey(nameof(Category))]
        public  int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<OrderProducts>? OrderProducts { get; set; }



    }
}
