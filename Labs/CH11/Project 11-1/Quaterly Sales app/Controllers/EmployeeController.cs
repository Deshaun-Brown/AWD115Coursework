using Microsoft.AspNetCore.Mvc;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Controllers
{
    public class EmployeeController : Controller
    {
        private QuarterlySalesContext context;

        public EmployeeController(QuarterlySalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Employees = context.Employees.ToList();
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                TempData["message"] = $"Employee {employee.Firstname} {employee.Lastname} added";
                return RedirectToAction("Add");
            }

            ViewBag.Employees = context.Employees.ToList();
            return View(employee);
        }
    }
}