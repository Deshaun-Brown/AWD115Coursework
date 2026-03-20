using System.ComponentModel.DataAnnotations;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Validation
{
    public class UniqueSalesAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(QuarterlySalesContext)) as QuarterlySalesContext;
            if (context == null) return ValidationResult.Success;

            var salesToValidate = (Sales)validationContext.ObjectInstance;

            var existingSale = context.Sales.FirstOrDefault(s =>
                s.Quarter == salesToValidate.Quarter &&
                s.Year == salesToValidate.Year &&
                s.EmployeeId == salesToValidate.EmployeeId);

            if (existingSale != null && existingSale.SalesId != salesToValidate.SalesId)
            {
                return new ValidationResult("Sales data for this exact quarter, year, and employee already exists in the database.");
            }

            return ValidationResult.Success;
        }
    }
}