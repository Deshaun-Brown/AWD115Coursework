using Microsoft.AspNetCore.Mvc;

namespace Chapter_7_Project.Areas.Help.Controllers;

[Area("Help")]
public class HomeController : Controller
{
    public IActionResult Index() => View();
}
