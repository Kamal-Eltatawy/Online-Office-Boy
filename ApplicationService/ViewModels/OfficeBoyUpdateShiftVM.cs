using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class OfficeBoyUpdateShiftVM
    {
        [Required]
        public string OfficeBoyId { get; set; }

        [Display(Name = "Office Boy Name")]
        public string? Name { get; set; }


        [Required]
        public TimeSpan ShiftStartTime { get; set; }

        [Required]
        public TimeSpan ShiftEndTime { get; set; }
    }
}
