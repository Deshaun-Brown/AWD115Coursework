using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Controllers;

public class CustomersController : Controller
{
    private readonly ApplicationDbContext _db;

    public CustomersController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Username)
            .ToListAsync();

        return View(customers);
    }

    public IActionResult Create() => View(new Customer());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
