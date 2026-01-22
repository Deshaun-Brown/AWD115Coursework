using Microsoft.AspNetCore.Mvc;
using Distance_Converter.Models;


namespace Distance_Converter.Controllers
{
    public class  DistanceController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View(new DistanceModel());

        }

        [HttpPost]
        public IActionResult Index(DistanceModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View(model);
        }
        
    }
}
