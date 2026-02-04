using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public IActionResult Index()
        {
            return View();
        }
    }
}
