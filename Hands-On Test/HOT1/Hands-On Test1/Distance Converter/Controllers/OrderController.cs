using Microsoft.AspNetCore.Mvc;
using Distance_Converter.Models;

namespace Distance_Converter.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new OrderModel());
        }

        [HttpPost]
        public IActionResult Index(OrderModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View(model);
        }
    }
}