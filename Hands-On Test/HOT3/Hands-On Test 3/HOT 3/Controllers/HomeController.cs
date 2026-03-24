using Microsoft.AspNetCore.Mvc;

namespace HOT_3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
