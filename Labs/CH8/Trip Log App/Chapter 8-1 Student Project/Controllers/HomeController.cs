using Chapter_8_1_Student_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chapter_8_1_Student_Project.Controllers;

public class HomeController : Controller
{
    private readonly TripsContext _context;

    public HomeController(TripsContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.SubHeader = string.Empty;
        var trips = await _context.Trips
            .OrderBy(t => t.StartDate)
            .ToListAsync();

        return View(trips);
    }
}
