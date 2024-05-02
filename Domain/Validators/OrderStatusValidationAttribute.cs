using Domain.Const;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validators
{
    public class OrderStatusValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (Enum.IsDefined(typeof(OrderStatus), value))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Invalid order status.");
        }
    }
}
