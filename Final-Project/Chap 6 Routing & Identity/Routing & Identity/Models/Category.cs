using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        public string Name { get; set; } = string.Empty;

        // Navigation Property (one Category -> many Products)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
