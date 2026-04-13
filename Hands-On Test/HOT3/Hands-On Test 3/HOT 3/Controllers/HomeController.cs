using Microsoft.AspNetCore.Mvc;

namespace Pharmaceuticals.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.ApplicationDbContext _context;

        public HomeController(Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mostExpensiveProduct = _context.Products.OrderByDescending(p => p.Price).FirstOrDefault();

            var model = new ViewModels.HomeIndexViewModel
            {
                MostExpensiveProductName = mostExpensiveProduct?.Name ?? string.Empty,
                MostExpensiveProductPrice = mostExpensiveProduct?.Price
            };

            return View(model);
        }
    }
}
