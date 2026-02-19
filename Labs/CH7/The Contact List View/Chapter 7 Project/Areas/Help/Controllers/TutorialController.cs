using Microsoft.AspNetCore.Mvc;

namespace Chapter_7_Project.Areas.Help.Controllers;

[Area("Help")]
public class TutorialController : Controller
{
    public IActionResult Index(string id = "Page1")
    {
        var viewName = id?.ToLowerInvariant() switch
        {
            "page1" or "1" => "Page1",
            "page2" or "2" => "Page2",
            "page3" or "3" => "Page3",
            _ => "Page1",
        };

        return View(viewName);
    }
}
