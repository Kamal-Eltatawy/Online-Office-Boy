using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OfficeBoyEditShiftRequestVM
    {
        [Required]
        public string OfficeBoyId { get; set; }

        [Required]

        public TimeSpan ShiftStartTime { get; set; }

        [Required]

        public TimeSpan ShiftEndTime { get; set; }
    }
}
