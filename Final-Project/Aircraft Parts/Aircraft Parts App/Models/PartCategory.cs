using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aircraft_Parts_App.Models
{
    public class PartCategory
    {
        public int PartCategoryId { get; set; }
        
        [Required]
        public int PartId { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        // Navigation properties
        [ValidateNever]
        [ForeignKey("PartId")]
        public virtual Part? Part { get; set; }
        
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
