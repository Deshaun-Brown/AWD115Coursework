using Chapter_8_1_Student_Project.Data;
using Chapter_8_1_Student_Project.Infrastructure;
using Chapter_8_1_Student_Project.Models;
using Chapter_8_1_Student_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Chapter_8_1_Student_Project.Controllers;

public class TripController : Controller
{
    private const string TripKey = "Trip";

    private readonly IRepository<Trip> _trips;
    private readonly IRepository<Destination> _destinations;
    private readonly IRepository<Accommodation> _accommodations;
    private readonly IRepository<Activity> _activities;

    public TripController(
        IRepository<Trip> trips,
        IRepository<Destination> destinations,
        IRepository<Accommodation> accommodations,
        IRepository<Activity> activities)
    {
        _trips = trips;
        _destinations = destinations;
        _accommodations = accommodations;
        _activities = activities;
    }

    [HttpGet]
    public IActionResult AddPage1()
    {
        ViewBag.SubHeader = string.Empty;
        ViewBag.Destinations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_destinations.List(new QueryOptions<Destination>()), "DestinationId", "Name");
        ViewBag.Accommodations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_accommodations.List(new QueryOptions<Accommodation>()), "AccommodationId", "Name");
        return View(new AddTripPage1ViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddPage1(AddTripPage1ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.SubHeader = string.Empty;
            ViewBag.Destinations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_destinations.List(new QueryOptions<Destination>()), "DestinationId", "Name");
            ViewBag.Accommodations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_accommodations.List(new QueryOptions<Accommodation>()), "AccommodationId", "Name");
            return View(model);
        }

        var trip = new Trip
        {
            DestinationId = model.DestinationId,
            AccommodationId = model.AccommodationId,
            StartDate = model.StartDate!.Value,
            EndDate = model.EndDate!.Value
        };

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

        var destination = _destinations.Get(trip.DestinationId);
        ViewBag.SubHeader = destination?.Name ?? string.Empty;
        ViewBag.Activities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_activities.List(new QueryOptions<Activity>()), "ActivityId", "Name");
        return View(new AddTripPage3ViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddPage3(AddTripPage3ViewModel model)
    {
        var trip = TempData.Get<Trip>(TripKey);
        if (trip is null)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            var destination = _destinations.Get(trip.DestinationId);
            ViewBag.SubHeader = destination?.Name ?? string.Empty;
            ViewBag.Activities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_activities.List(new QueryOptions<Activity>()), "ActivityId", "Name");
            return View(model);
        }

        var allActivities = _activities.List(new QueryOptions<Activity>());
        var selectedActivities = allActivities.Where(a => model.ActivityIds.Contains(a.ActivityId)).ToList();
        trip.Activities = selectedActivities;

        _trips.Insert(trip);
        _trips.Save();

        TempData.Clear();
        var dest = _destinations.Get(trip.DestinationId);
        TempData["Message"] = $"Trip to {dest?.Name} added.";

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var trip = _trips.Get(id);
        if (trip != null)
        {
            _trips.Delete(trip);
            _trips.Save();
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Cancel()
    {
        TempData.Clear();
        return RedirectToAction("Index", "Home");
    }
}
