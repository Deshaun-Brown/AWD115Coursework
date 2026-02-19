using Chapter_8_1_Student_Project.Data;
using Chapter_8_1_Student_Project.Infrastructure;
using Chapter_8_1_Student_Project.Models;
using Chapter_8_1_Student_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Chapter_8_1_Student_Project.Controllers;

public class TripController : Controller
{
    private const string TripKey = "Trip";

    private readonly TripsContext _context;

    public TripController(TripsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult AddPage1()
    {
        ViewBag.SubHeader = string.Empty;
        return View(new AddTripPage1ViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddPage1(AddTripPage1ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.SubHeader = string.Empty;
            return View(model);
        }

        var trip = new Trip
        {
            Destination = model.Destination,
            Accommodation = model.Accommodation,
            StartDate = model.StartDate!.Value,
            EndDate = model.EndDate!.Value
        };

        TempData.Put(TripKey, trip);
        return RedirectToAction(nameof(AddPage2));
    }

    [HttpGet]
    public IActionResult AddPage2()
    {
        var trip = TempData.Peek<Trip>(TripKey);
        if (trip is null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.SubHeader = trip.Accommodation;
        return View(new AddTripPage2ViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddPage2(AddTripPage2ViewModel model)
    {
        var trip = TempData.Get<Trip>(TripKey);
        if (trip is null)
        {
            return RedirectToAction("Index", "Home");
        }

        trip.AccommodationPhone = model.AccommodationPhone;
        trip.AccommodationEmail = model.AccommodationEmail;

        TempData.Put(TripKey, trip);
        return RedirectToAction(nameof(AddPage3));
    }

    [HttpGet]
    public IActionResult AddPage3()
    {
        var trip = TempData.Peek<Trip>(TripKey);
        if (trip is null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.SubHeader = trip.Destination;
        return View(new AddTripPage3ViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPage3(AddTripPage3ViewModel model)
    {
        var trip = TempData.Get<Trip>(TripKey);
        if (trip is null)
        {
            return RedirectToAction("Index", "Home");
        }

        trip.Activity1 = model.Activity1;
        trip.Activity2 = model.Activity2;
        trip.Activity3 = model.Activity3;

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        TempData.Clear();
        TempData["Message"] = $"Trip to {trip.Destination} added.";

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Cancel()
    {
        TempData.Clear();
        return RedirectToAction("Index", "Home");
    }
}
