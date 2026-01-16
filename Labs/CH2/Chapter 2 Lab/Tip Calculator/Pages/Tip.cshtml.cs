using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TipCalculator.Models;

namespace TipCalculator.Pages
{
    public class TipModel : PageModel
    {
        [BindProperty]
        public MealCost Meal { get; set; } = new MealCost();

        public void OnGet()
        {
            // Empty form on first load
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Show validation errors
            }

            return Page(); // Show calculated tips
        }

        public IActionResult OnGetClear()
        {
            Meal = new MealCost();
            ModelState.Clear();
            return RedirectToPage();
        }
    }
}