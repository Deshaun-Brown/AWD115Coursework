using System.ComponentModel.DataAnnotations;

namespace FAQ.Models
{
    public class Category
    {
        [Key]
        [StringLength(20)]
        public string CategoryId { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
