using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OfficeBoyResponseVM
    {
        [Required]
        public string OfficeBoyId { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; }
   
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string PaidPeriod { get; set; }

        public string OfficeLocation { get; set; }
        
        public int? ShiftId { get; set; }

        [Required]
        public TimeSpan ShiftStartTime { get; set; }

        [Required]
        public TimeSpan ShiftEndTime { get; set; }

    }
}
