using System.ComponentModel.DataAnnotations;

namespace Quaterly_Sales_app.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfHire { get; set; }

        public int ManagerId { get; set; }
        
        public ICollection<Sales> Sales { get; set; } = new List<Sales>();
    }
}
