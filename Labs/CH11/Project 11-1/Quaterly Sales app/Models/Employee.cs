using System.ComponentModel.DataAnnotations;
using Quaterly_Sales_app.Validation;

namespace Quaterly_Sales_app.Models
{
    [UniqueEmployee]
    [ValidManager]
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Date of birth must be a valid date in the past.")]
        public DateTime DOB { get; set; }
        
        [Required(ErrorMessage = "Date of Hire is required.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Date of hire must be a valid date in the past.")]
        [HireDate]
        [ValidHireDate]
        public DateTime DateOfHire { get; set; }

        [Required(ErrorMessage = "Manager is required.")]
        public int ManagerId { get; set; }

        public ICollection<Sales> Sales { get; set; } = new List<Sales>();

        internal class validationContext
        {
            internal class ObjectInstance;
        }
    }
}
