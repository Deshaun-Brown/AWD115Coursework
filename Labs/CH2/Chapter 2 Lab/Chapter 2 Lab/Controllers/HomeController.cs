using Microsoft.AspNetCore.Mvc;
using Chapter_2_Lab.Models;

namespace Chapter_2_Lab.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new PriceQuote());

        }

        [HttpPost]
        public IActionResult Index(PriceQuote  model)
        {
            if (!ModelState.IsValid)// Validate user input
            {
                return View(model); // Show validation errors
            }

            return View(model);// Show calculated result
        }

        public IActionResult Clear()
        {
            return RedirectToAction("Index"); //Reset form
        }



    }
}
