using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aircraft_Parts_App.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        // Navigation property - Many Categories have Many Parts (through PartCategory)
        [ValidateNever]
        public virtual ICollection<PartCategory> PartCategories { get; set; } = new List<PartCategory>();
    }
}
