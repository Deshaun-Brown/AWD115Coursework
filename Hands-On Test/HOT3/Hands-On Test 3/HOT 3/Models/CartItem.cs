using System.ComponentModel.DataAnnotations;

namespace Pharmaceuticals.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Medication Name")]
        public string? Accedameitphin { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
