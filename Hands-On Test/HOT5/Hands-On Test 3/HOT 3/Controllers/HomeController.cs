using Microsoft.AspNetCore.Mvc;
using Pharmaceuticals.ViewModels; // Add this using directive

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
            var products = _context.Products.ToList();
            var viewModel = new HomeIndexViewModel
            {
                Products = products
                // set other properties if needed
            };
            return View(viewModel);
        }
    }
}
