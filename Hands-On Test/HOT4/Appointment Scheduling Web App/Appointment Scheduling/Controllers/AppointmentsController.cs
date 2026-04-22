using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace Appointment_Scheduling.Controllers;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IValidator<Appointment> _validator;

    public AppointmentsController(ApplicationDbContext db, IValidator<Appointment> validator)
    {
        _db = db;
        _validator = validator;
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
        return View(new Appointment { StartDateTime = GetNextWholeHour() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (appointment.StartDateTime <= DateTime.Now)
        {
            ModelState.AddModelError(nameof(Appointment.StartDateTime), "Appointment start date/time must be in the future.");
        }

        var validationResult = await _validator.ValidateAsync(appointment);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        if (!ModelState.IsValid)
        {
            await PopulateCustomersAsync();
            return View(appointment);
        }

        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = "Appointment created successfully.";
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

    private static DateTime GetNextWholeHour()
    {
        var now = DateTime.Now.AddHours(1);
        return new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
    }
}
