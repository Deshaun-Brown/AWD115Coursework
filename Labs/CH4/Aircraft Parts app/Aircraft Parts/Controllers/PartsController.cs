using Microsoft.AspNetCore.Mvc;

namespace Aircraft_Part.Controllers
{
    public class PartsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
