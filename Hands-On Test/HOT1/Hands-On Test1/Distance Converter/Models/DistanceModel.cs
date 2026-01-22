using System.ComponentModel.DataAnnotations;

namespace Distance_Converter.Models
{
    public class DistanceModel
    {
        [Required(ErrorMessage = "Please enter a distance in inches.")]
        [Range(1, 500, ErrorMessage = "Distance must be between 1 and 500.")]
        public decimal? Inches { get; set; }

        public decimal Centimeters =>
            Inches.HasValue ? Inches.Value * 2.54m : 0;
    }
}