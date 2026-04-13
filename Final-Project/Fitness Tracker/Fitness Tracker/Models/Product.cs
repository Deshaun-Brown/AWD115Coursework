using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // FK + Navigation (many Products -> one Category)
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        // Computed property for URL slug
        public string Slug => Name?.Replace(' ', '-').ToLower() ?? string.Empty;
    }
}
