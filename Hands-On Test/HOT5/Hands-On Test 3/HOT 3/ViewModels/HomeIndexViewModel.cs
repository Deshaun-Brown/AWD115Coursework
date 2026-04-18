using Pharmaceuticals.Models;

namespace Pharmaceuticals.ViewModels;

public class HomeIndexViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public int ProductCount => Products?.Count() ?? 0;
    public decimal TotalValue => Products?.Sum(p => p.Price * p.Quantity) ?? 0;
    public int LowStockCount => Products?.Count(p => p.Quantity < 5) ?? 0;


}


