using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /orders
        [HttpGet("/orders", Name = "Orders")]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(orders);
        }

        // GET: /orders/create
        [HttpGet("/orders/create", Name = "OrdersCreate")]
        public IActionResult Create()
        {
            return View(new Order());
        }

        [HttpPost("/orders/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
