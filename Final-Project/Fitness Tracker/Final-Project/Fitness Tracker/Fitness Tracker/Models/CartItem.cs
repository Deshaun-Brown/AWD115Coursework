using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
