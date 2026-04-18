using System.Collections.Generic;
using Pharmaceuticals.Models;

namespace Pharmaceuticals.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
