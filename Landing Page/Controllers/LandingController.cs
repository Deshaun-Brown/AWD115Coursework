using Microsoft.AspNetCore.Mvc;

namespace Landing_Page.Controllers
{
    public class LandingController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
