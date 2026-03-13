using System.Collections.Generic;
using System.Linq;
using Fitness_Tracker.Models;

namespace Fitness_Tracker.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public decimal LineTotal => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal Subtotal => Items.Sum(i => i.LineTotal);
        public decimal TaxRate => 0.07m;
        public decimal Tax => decimal.Round(Subtotal * TaxRate, 2);
        public decimal Total => Subtotal + Tax;
    }
}
