using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOT_3.Models;


namespace HOT_3.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public SelectList ManagerList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ManagerList = new SelectList(await _context.Employees.ToListAsync(), "EmployeeId", "LastName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Employee.EmployeeId == Employee.ManagerId)
            {
                ModelState.AddModelError("Employee.ManagerId", "Employee and manager may not be the same person.");
            }

            var duplicate = await _context.Employees
                .FirstOrDefaultAsync(e => e.FirstName == Employee.FirstName && e.LastName == Employee.LastName && e.DOB == Employee.DOB);

            if (duplicate != null)
            {
                ModelState.AddModelError("", "An employee with the same first name, last name, and date of birth already exists.");
            }

            var managerDuplicate = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == Employee.ManagerId && e.FirstName == Employee.FirstName && e.LastName == Employee.LastName && e.DOB == Employee.DOB);

            if (managerDuplicate != null)
            {
                ModelState.AddModelError("", "A new employee may not have a manager with the same first name, last name, and date of birth.");
            }

            if (!ModelState.IsValid)
            {
                ManagerList = new SelectList(await _context.Employees.ToListAsync(), "EmployeeId", "LastName");
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}