using Domain.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class ReportHeaderSearchRequestVM
    {
        [Required]
        public SearchOptions SearchOptions {  get; set; } 

        public bool IsPaid { get; set; } = false;

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        
        public string? UserId { get; set; }
    }
}
