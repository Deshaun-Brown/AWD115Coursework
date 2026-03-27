using Pharmaceuticals.Models;

namespace Pharmaceuticals.ViewModels;

public class HomeIndexViewModel
{
    public int ProductCount { get; set; }
    public List<Product> RecentProducts { get; set; } = new();

    // Computed answer shown on the home page: most expensive product
    public string MostExpensiveProductName { get; set; } = string.Empty;
    public decimal? MostExpensiveProductPrice { get; set; }
}
