using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Controllers;

public class CustomersController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IValidator<Customer> _validator;

    public CustomersController(ApplicationDbContext db, IValidator<Customer> validator)
    {
        _db = db;
        _validator = validator;
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
        var validationResult = await _validator.ValidateAsync(customer);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = "Customer created successfully.";
        return RedirectToAction(nameof(Index));
    }
}
