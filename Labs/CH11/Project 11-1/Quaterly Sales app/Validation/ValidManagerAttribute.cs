using System.ComponentModel.DataAnnotations;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Validation
{
    public class ValidManagerAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(QuarterlySalesContext)) as QuarterlySalesContext;
            if (context == null) return ValidationResult.Success;

            var employeeToValidate = (Employee)validationContext.ObjectInstance;

            if (employeeToValidate.ManagerId > 0)
            {
                var manager = context.Employees.Find(employeeToValidate.ManagerId);
                if (manager != null)
                {
                    if (manager.Firstname.ToLower() == employeeToValidate.Firstname.ToLower() &&
                        manager.Lastname.ToLower() == employeeToValidate.Lastname.ToLower() &&
                        manager.DOB.Date == employeeToValidate.DOB.Date)
                    {
                        return new ValidationResult("Employee and manager may not be the same person.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}