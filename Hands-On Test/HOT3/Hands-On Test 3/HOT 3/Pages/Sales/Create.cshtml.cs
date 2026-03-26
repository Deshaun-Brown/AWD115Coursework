using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOT_3.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HOT_3.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SalesData SalesData { get; set; }

        public SelectList EmployeeList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EmployeeList = new SelectList(await _context.Employees.ToListAsync(), "EmployeeId", "LastName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var duplicate = await _context.SalesData
                .FirstOrDefaultAsync(s => s.Quarter == SalesData.Quarter && s.Year == SalesData.Year && s.EmployeeId == SalesData.EmployeeId);

            if (duplicate != null)
            {
                ModelState.AddModelError("", "Sales data for this exact quarter, year, and employee already exists.");
            }

            if (!ModelState.IsValid)
            {
                EmployeeList = new SelectList(await _context.Employees.ToListAsync(), "EmployeeId", "LastName");
                return Page();
            }

            _context.SalesData.Add(SalesData);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}