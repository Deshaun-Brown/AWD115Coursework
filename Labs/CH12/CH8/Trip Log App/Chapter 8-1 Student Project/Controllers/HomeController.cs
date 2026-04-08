using Chapter_8_1_Student_Project.Data;
using Chapter_8_1_Student_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chapter_8_1_Student_Project.Controllers;

public class HomeController : Controller
{
    private readonly IRepository<Trip> _trips;

    public HomeController(IRepository<Trip> trips)
    {
        _trips = trips;
    }

    public IActionResult Index()
    {
        ViewBag.SubHeader = string.Empty;
        var trips = _trips.List(new QueryOptions<Trip>
        {
            Includes = "Destination, Accommodation, Activities",
            OrderBy = t => t.StartDate
        });

        return View(trips);
    }
}
