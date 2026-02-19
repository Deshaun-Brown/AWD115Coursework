using Microsoft.AspNetCore.Mvc;

namespace Chapter_7_Project.Controllers;

public record ContactInfo(string Name, string Email, string Phone);

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult About() => View();

    public IActionResult Contact()
    {
        var contacts = new List<ContactInfo>
        {
            new("Customer Support", "support@example.com", "555-0100"),
            new("Sales", "sales@example.com", "555-0110"),
            new("Human Resources", "hr@example.com", "555-0120")
        };

        return View(contacts);
    }
}
