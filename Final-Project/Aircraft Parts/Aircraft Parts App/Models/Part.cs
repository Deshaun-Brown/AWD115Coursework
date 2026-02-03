using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aircraft_Parts_App.Models
{
    public class Part
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "NIIN")]
        public string NIIN { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Part Name")]
        public string PartName { get; set; } = string.Empty;
        
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }
        
        // Foreign Key for One-To-Many with Supplier
        [Required]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [NotMapped]
        public string Slug => $"{NIIN}-{Manufacturer?.ToLower()}-{PartName?.ToLower()}";

        // Navigation properties
        [ValidateNever] 
        public virtual PartDetails? PartsDetails { get; set; }
        
        [ValidateNever]
        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }
        
        [ValidateNever]
        public virtual ICollection<PartCategory> PartCategories { get; set; } = new List<PartCategory>();
    }
}
