using System.ComponentModel.DataAnnotations;

namespace Quaterly_Sales_app.Validation
{
    public class HireDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateToCheck)
            {
                DateTime companyFounding = new DateTime(1995, 1, 1);
                if (dateToCheck < companyFounding)
                {
                    return new ValidationResult(ErrorMessage ?? "Date of hire cannot be before the company was founded (1/1/1995).");
                }
            }
            return ValidationResult.Success;
        }
    }
}