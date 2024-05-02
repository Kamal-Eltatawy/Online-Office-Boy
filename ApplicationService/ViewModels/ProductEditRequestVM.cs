using ApplicationService.Custome_Validation_Attribute;
using Domain.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class ProductEditRequestVM
    {
        [Required]
        public int ProductId { get; set; }  
        [Required, MaxLength(50)]

        public string Name { get; set; }
        [Required, MaxLength(100)]

        public string Description { get; set; }

        [Required, GreaterThanZeroDecimal]

        public decimal Price { get; set; }

        [ ImageAllowedExtintion(".jpg", ".jpeg", ".png", ErrorMessage = "Unsupported Media")]
        public IFormFile? File { get; set; }


        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$", ErrorMessage = "Invalid time span format.")]
        public TimeSpan ReadyTime { get; set; }

        public string? PictureUrl { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
