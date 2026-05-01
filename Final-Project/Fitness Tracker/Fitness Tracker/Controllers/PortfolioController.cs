using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers;

public class PortfolioController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
