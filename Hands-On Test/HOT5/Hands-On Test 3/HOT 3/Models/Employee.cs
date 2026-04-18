
using System.ComponentModel.DataAnnotations;


namespace Pharmaceuticals.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [PastDateAttribute(ErrorMessage = "Date of Birth must be in the past.")]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Hire")]
        [PastDateAttribute(ErrorMessage = "Date of Hire must be in the past.")]
        [CompanyFoundedDate(ErrorMessage = "Date of Hire must not be before 1/1/2000.")]
        public DateTime DateOfHire { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }
        
        public ICollection<SalesData> Sales { get; set; }
    }
}
