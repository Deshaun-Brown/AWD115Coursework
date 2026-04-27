using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmaceuticals.Data;
using Pharmaceuticals.ViewModels;

namespace Pharmaceuticals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .ToList();
            var viewModel = new HomeIndexViewModel
            {
                Products = products
            };
            return View(viewModel);
        }
    }
}
