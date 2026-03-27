using Microsoft.AspNetCore.Mvc;

namespace Pharmaceuticals.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
