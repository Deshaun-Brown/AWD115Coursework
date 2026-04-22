using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Areas.Admin.Controllers;

[Area("Admin")]
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

    public async Task<IActionResult> Edit(int id)
    {
        var appt = await _db.Appointments.FindAsync(id);
        if (appt is null) return NotFound();

        await PopulateCustomersAsync(appt.CustomerId);
        return View(appt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Appointment appointment)
    {
        if (id != appointment.Id) return BadRequest();

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
            await PopulateCustomersAsync(appointment.CustomerId);
            return View(appointment);
        }

        _db.Entry(appointment).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var appt = await _db.Appointments
            .AsNoTracking()
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appt is null) return NotFound();
        return View(appt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appt = await _db.Appointments.FindAsync(id);
        if (appt is null) return NotFound();

        _db.Appointments.Remove(appt);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var appt = await _db.Appointments
            .AsNoTracking()
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appt is null) return NotFound();
        return View(appt);
    }

    private async Task PopulateCustomersAsync(int? selectedCustomerId = null)
    {
        var customers = await _db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Username)
            .ToListAsync();

        ViewBag.CustomerId = new SelectList(customers, nameof(Customer.Id), nameof(Customer.Username), selectedCustomerId);
    }
}
