using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Controllers;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _db;

    public AppointmentsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var appointments = await _db.Appointments
            .AsNoTracking()
            .Include(a => a.Customer)
            .OrderBy(a => a.StartDateTime)
            .ToListAsync();

        return View(appointments);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCustomersAsync();
        return View(new Appointment { StartDateTime = DateTime.Now.AddDays(1).Date.AddHours(DateTime.Now.Hour + 1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCustomersAsync();
            return View(appointment);
        }

        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCustomersAsync()
    {
        var customers = await _db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Username)
            .ToListAsync();

        ViewBag.CustomerId = new SelectList(customers, nameof(Customer.Id), nameof(Customer.Username));
    }
}
