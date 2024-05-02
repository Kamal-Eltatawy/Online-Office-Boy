using Domain.Const;
using Domain.Validators;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public  int  PaidPeriod { get; set; }

        [ForeignKey(nameof(Office))]
        public int OfficeId { get; set; }

        [ForeignKey(nameof(Shifts))]

        public int? ShiftId { get; set; }

        public virtual Shifts Shifts { get; set; }
        public virtual Office Office { get; set; }

        public virtual List<Order> Orders { get; set; }


    }
}
