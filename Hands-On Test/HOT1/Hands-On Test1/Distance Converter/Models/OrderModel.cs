using System.ComponentModel.DataAnnotations;

namespace Distance_Converter.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; }

        public string? DiscountCode { get; set; }

        public decimal ShirtPrice => 15m;
        public decimal TaxRate => 0.08m;

        public decimal DiscountPercent
        {
            get
            {
                return DiscountCode?.ToUpper() switch
                {
                    "6175" => 0.30m,
                    "1390" => 0.20m,
                    "BB88" => 0.10m,
                    _ => 0m
                };
            }
        }

        public bool IsDiscountValid =>
            DiscountCode == "6175" ||
            DiscountCode == "1390" ||
            DiscountCode?.ToUpper() == "BB88";

        public decimal Subtotal =>
            Quantity.HasValue ? Quantity.Value * ShirtPrice : 0;

        public decimal DiscountAmount =>
            IsDiscountValid ? Subtotal * DiscountPercent : 0;

        public decimal Tax =>
            (Subtotal - DiscountAmount) * TaxRate;

        public decimal Total =>
            Subtotal - DiscountAmount + Tax;
    }
}