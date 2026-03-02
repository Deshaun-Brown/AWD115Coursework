using Fitness_Tracker.Models;

namespace Fitness_Tracker.ViewModels;

public class HomeIndexViewModel
{
    public int ProductCount { get; set; }
    public List<Product> RecentProducts { get; set; } = new();
    // Whether the OpenAI API key is configured (basic readiness check)
    public bool AiReady { get; set; }

    // Computed answer shown on the home page: most expensive product
    public string MostExpensiveProductName { get; set; } = string.Empty;
    public decimal? MostExpensiveProductPrice { get; set; }
}
