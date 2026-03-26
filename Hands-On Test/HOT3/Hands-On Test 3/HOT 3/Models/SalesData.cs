using System.ComponentModel.DataAnnotations;

namespace HOT_3.Models
{
    public class SalesData
    {
        public int SalesDataId { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Quarter must be between 1 and 4.")]
        public int Quarter { get; set; }

        [Required]
        [Range(2001, int.MaxValue, ErrorMessage = "Year must be after the year 2000.")]
        public int Year { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        
        public Employee Employee { get; set; }
    }
}