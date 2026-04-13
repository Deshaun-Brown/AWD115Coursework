
using Pharmaceuticals.Models;

namespace Pharmaceuticals.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string SelectedCategory { get; set; } = "all";
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}
