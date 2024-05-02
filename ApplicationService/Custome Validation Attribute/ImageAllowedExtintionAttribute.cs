using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApplicationService.Custome_Validation_Attribute
{
    public class ImageAllowedExtintionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public ImageAllowedExtintionAttribute(params string[] extentions)
        {
            this._extensions = extentions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
        {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(fileExtension))
                {
                    return new ValidationResult($"The file type {fileExtension} is not allowed.");
                }
            }

            return ValidationResult.Success;
        }
    }
    }

