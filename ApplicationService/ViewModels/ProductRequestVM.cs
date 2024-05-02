using Domain.Entities;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationService.Custome_Validation_Attribute;

namespace ApplicationService.ViewModels
{
    public class ProductRequestVM
    {
        [Required, MaxLength(50)]

        public string Name { get; set; }
        [Required, MaxLength(100)]

        public string Description { get; set; }

        [Required, GreaterThanZeroDecimal]

        public decimal Price { get; set; }

        [Required, ImageAllowedExtintion(".jpg", ".jpeg", ".png", ErrorMessage = "Unsupported Media")]
        public IFormFile File { get; set; }


        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$", ErrorMessage = "Invalid time span format.")]
        public string ReadyTime { get; set; }
        [Required]
        [Display(Name= "Category")]
        public  int CategoryId { get; set; }

    }
}
