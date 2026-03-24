using System.Collections.Generic;
using HOT_3.Models;

using System.Collections.Generic;
using HOT_3.Models;

namespace HOT_3.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string SelectedCategory { get; set; } = "all";
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}
