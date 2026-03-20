using System.ComponentModel.DataAnnotations;

namespace Quaterly_Sales_app.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateToCheck)
            {
                if (dateToCheck >= DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }
}