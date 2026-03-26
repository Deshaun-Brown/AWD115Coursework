using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.Controllers
{
    public class HomeController : Controller
    {
        private QuarterlySalesContext context;

        public HomeController(QuarterlySalesContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(int id)
        {
            var sales = context.Sales.Include(s => s.Employee).ToList();

            if (id > 0)
            {
                sales = sales.Where(s => s.EmployeeId == id).ToList();
            }

            var employees = context.Employees.ToList();

            // Insert 'All' employee selection option at the top of the list
            employees.Insert(0, new Employee { EmployeeId = 0, Firstname = "All", Lastname = string.Empty });
            
            var viewModel = new Quaterly_Sales_app.ViewModels.SalesListViewModel
            {
                Sales = sales,
                Employees = employees,
                EmployeeId = id
            };

            return View(viewModel);
        }
    }
}