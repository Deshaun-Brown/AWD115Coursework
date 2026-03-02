using Fitness_Tracker.Data;
using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDatabaseAgent _agent;

        public HomeController(IDatabaseAgent agent)
        {
            _agent = agent;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            var products = await _agent.GetAllProductsAsync();
            var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();

            var model = new HomeIndexViewModel
            {
                ProductCount = await _agent.GetProductCountAsync(),
                RecentProducts = products.Take(5).ToList(),
                AiReady = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FITNESS_TRACKER_API_KEY")),
                MostExpensiveProductName = mostExpensive?.Name ?? string.Empty,
                MostExpensiveProductPrice = mostExpensive?.Price
            };

            return View(model);
        }
    }
}
