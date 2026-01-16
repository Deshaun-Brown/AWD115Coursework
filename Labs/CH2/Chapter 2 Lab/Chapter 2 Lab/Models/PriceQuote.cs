using System.ComponentModel.DataAnnotations;


namespace Chapter_2_Lab.Models
{

    public class PriceQuote
    {
        [Required(ErrorMessage = "Subtotal is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Subtotal must be greater than 0.")]
        public decimal? Subtotal { get; set; }

        [Required(ErrorMessage = "Discount percent is required.")]
        [Range(0.01, 100, ErrorMessage = "Discount percent must be between 0 and 100.")]
        public decimal? DiscountPercent { get; set; }

        public decimal DiscountAmount => Subtotal.HasValue && DiscountPercent.HasValue
            ? Subtotal.Value * DiscountPercent.Value / 100
            : 0;

        public decimal Total => Subtotal.HasValue && DiscountPercent.HasValue
            ? Subtotal.Value - (Subtotal.Value * DiscountPercent.Value / 100)
            : 0;
    };


}