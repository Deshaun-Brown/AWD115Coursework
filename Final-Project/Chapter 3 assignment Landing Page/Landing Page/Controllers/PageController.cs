using Microsoft.AspNetCore.Mvc;

namespace Landing_Page.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
