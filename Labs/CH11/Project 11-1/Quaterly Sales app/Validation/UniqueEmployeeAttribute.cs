using System.ComponentModel.DataAnnotations;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Validation
{
    public class UniqueEmployeeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(QuarterlySalesContext)) as QuarterlySalesContext;
            if (context == null) return ValidationResult.Success;

            // Safely attempt to cast to Employee from either value or ObjectInstance
            var employeeToValidate = value as Employee ?? validationContext.ObjectInstance as Employee;
            
            if (employeeToValidate == null)
            {
                return ValidationResult.Success;
            }

            var existingEmployee = context.Employees.FirstOrDefault(e =>
                e.Firstname.ToLower() == employeeToValidate.Firstname.ToLower() &&
                e.Lastname.ToLower() == employeeToValidate.Lastname.ToLower() &&
                e.DOB.Date == employeeToValidate.DOB.Date);

            if (existingEmployee != null && existingEmployee.EmployeeId != employeeToValidate.EmployeeId)
            {
                return new ValidationResult("An employee with the same first name, last name, and date of birth is already in the database.");
            }

            return ValidationResult.Success;
        }
    }
}