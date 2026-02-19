using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aircraft_Parts_App.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string SupplierName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;
        
        [Phone]
        public string? PhoneNumber { get; set; }
        
        public string? Address { get; set; }
        
        // Navigation property - One Supplier has Many Parts
        [ValidateNever]
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
