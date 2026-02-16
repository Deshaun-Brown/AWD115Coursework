using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(1, 100000, ErrorMessage = "Price must be between $1 and $100,000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        // FK + Navigation (many Products -> one Category)
        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        // Computed property for URL slug
        public string Slug => Name?.Replace(' ', '-').ToLower() ?? string.Empty;
    }
}
