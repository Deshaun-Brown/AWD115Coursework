using Appointment_Scheduling.Areas.Admin.Models;
using Appointment_Scheduling.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var now = DateTime.Now;
        var model = new AdminDashboardViewModel
        {
            AppointmentCount = await _db.Appointments.CountAsync(),
            CustomerCount = await _db.Customers.CountAsync(),
            UpcomingAppointmentCount = await _db.Appointments.CountAsync(a => a.StartDateTime >= now)
        };

        return View(model);
    }
}
