using System.ComponentModel.DataAnnotations;

namespace TipCalculator.Models
{
    public class MealCost
    {
        [Required(ErrorMessage = "Meal cost is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Meal cost must be greater than 0.")]
        public decimal? Cost { get; set; }

        public decimal Tip15 => Cost.HasValue ? Math.Round(Cost.Value * 0.15m, 2) : 0m;
        public decimal Tip20 => Cost.HasValue ? Math.Round(Cost.Value * 0.20m, 2) : 0m;
        public decimal Tip25 => Cost.HasValue ? Math.Round(Cost.Value * 0.25m, 2) : 0m;
    }
}