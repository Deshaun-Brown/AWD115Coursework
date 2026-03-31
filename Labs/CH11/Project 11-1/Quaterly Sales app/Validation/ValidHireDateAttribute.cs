using System.ComponentModel.DataAnnotations;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Validation
{
    public class ValidHireDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;
            if (employee == null)
            {
                return ValidationResult.Success;
            }

            if (employee.DOB == default || employee.DateOfHire == default)
            {
                return ValidationResult.Success; // Let Required attribute handle these cases
            }

            if (employee.DateOfHire <= employee.DOB)
            {
                return new ValidationResult("Date of hire must be after the date of birth.");
            }

            return ValidationResult.Success;
        }
    }
}