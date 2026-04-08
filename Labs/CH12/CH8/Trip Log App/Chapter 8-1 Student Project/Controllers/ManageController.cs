using Chapter_8_1_Student_Project.Data;
using Chapter_8_1_Student_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chapter_8_1_Student_Project.Controllers
{
    public class ManageController : Controller
    {
        private readonly IRepository<Destination> _destinations;
        private readonly IRepository<Accommodation> _accommodations;
        private readonly IRepository<Activity> _activities;

        public ManageController(
            IRepository<Destination> destinations,
            IRepository<Accommodation> accommodations,
            IRepository<Activity> activities)
        {
            _destinations = destinations;
            _accommodations = accommodations;
            _activities = activities;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Destinations
        public IActionResult Destinations() => View(_destinations.List(new QueryOptions<Destination>()));

        public IActionResult AddDestination() => View("Destination", new Destination());

        [HttpPost]
        public IActionResult AddDestination(Destination model)
        {
            ModelState.Remove("Trips");
            if (ModelState.IsValid)
            {
                _destinations.Insert(model);
                _destinations.Save();
                return RedirectToAction("Destinations");
            }
            return View("Destination", model);
        }

        [HttpPost]
        public IActionResult DeleteDestination(int id)
        {
            var entity = _destinations.Get(id);
            if (entity != null)
            {
                try
                {
                    _destinations.Delete(entity);
                    _destinations.Save();
                }
                catch
                {
                    TempData["Message"] = $"Cannot delete {entity.Name} because it is associated with a trip.";
                }
            }
            return RedirectToAction("Destinations");
        }

        // Accommodations
        public IActionResult Accommodations() => View(_accommodations.List(new QueryOptions<Accommodation>()));

        public IActionResult AddAccommodation() => View("Accommodation", new Accommodation());

        [HttpPost]
        public IActionResult AddAccommodation(Accommodation model)
        {
            ModelState.Remove("Trips");
            if (ModelState.IsValid)
            {
                _accommodations.Insert(model);
                _accommodations.Save();
                return RedirectToAction("Accommodations");
            }
            return View("Accommodation", model);
        }

        [HttpPost]
        public IActionResult DeleteAccommodation(int id)
        {
            var entity = _accommodations.Get(id);
            if (entity != null)
            {
                try
                {
                    _accommodations.Delete(entity);
                    _accommodations.Save();
                }
                catch
                {
                    TempData["Message"] = $"Cannot delete {entity.Name} because it is associated with a trip.";
                }
            }
            return RedirectToAction("Accommodations");
        }

        // Activities
        public IActionResult Activities() => View(_activities.List(new QueryOptions<Activity>()));

        public IActionResult AddActivity() => View("Activity", new Activity());

        [HttpPost]
        public IActionResult AddActivity(Activity model)
        {
            ModelState.Remove("Trips");
            if (ModelState.IsValid)
            {
                _activities.Insert(model);
                _activities.Save();
                return RedirectToAction("Activities");
            }
            return View("Activity", model);
        }

        [HttpPost]
        public IActionResult DeleteActivity(int id)
        {
            var entity = _activities.Get(id);
            if (entity != null)
            {
                try
                {
                    _activities.Delete(entity);
                    _activities.Save();
                }
                catch
                {
                    TempData["Message"] = $"Cannot delete {entity.Name} because it is associated with a trip.";
                }
            }
            return RedirectToAction("Activities");
        }
    }
}