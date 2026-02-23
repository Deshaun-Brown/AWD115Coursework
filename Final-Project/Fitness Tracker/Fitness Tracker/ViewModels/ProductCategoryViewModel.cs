using System.Collections.Generic;
using Fitness_Tracker.Models;

using System.Collections.Generic;
using Fitness_Tracker.Models;

namespace Fitness_Tracker.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string SelectedCategory { get; set; } = "all";
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}
