
using System.ComponentModel.DataAnnotations;

namespace Pharmaceuticals.Models
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date >= DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class CompanyFoundedDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date < new DateTime(1995, 1, 1))
                {
                    return new ValidationResult(ErrorMessage ?? "Date must not be before 1/1/1995.");
                }
            }
            return ValidationResult.Success;
        }
    }
}