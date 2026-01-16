using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TipCalculator.Models;

namespace Tip_Calculator.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public MealCost Meal { get; set; } = new MealCost();


        public void OnGet()
        {

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
