using Microsoft.AspNetCore.Mvc;

namespace Aircraft_Parts_App.Controllers
{
    public class PartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
