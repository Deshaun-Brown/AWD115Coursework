using Microsoft.AspNetCore.Mvc;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Controllers
{
    public class SalesController : Controller
    {
        private QuarterlySalesContext context;

        public SalesController(QuarterlySalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Employees = context.Employees.ToList();
            return View(new Sales());
        }

        [HttpPost]
        public IActionResult Add(Sales sales)
        {
            if (ModelState.IsValid)
            {
                context.Sales.Add(sales);
                context.SaveChanges();
                TempData["message"] = $"Sales amount of {sales.Amount:c} added for Quarter {sales.Quarter} {sales.Year}.";
                return RedirectToAction("Add");
            }

            ViewBag.Employees = context.Employees.ToList();
            return View(sales);
        }
    }
}