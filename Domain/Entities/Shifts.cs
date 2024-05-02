using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shifts
    {
        public int  ID {  get; set; }

        [Column(TypeName = "time")]
        public TimeSpan ShiftStartTime { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan ShiftEndTime { get; set; }

        public virtual List<User>? Users { get; set; }
    }
}
