using Bookstore_App.Models.DomainModels;

namespace Bookstore_App.Models.ViewModels;

public class CartItemViewModel
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity;
}

public class CartViewModel
{
    public IList<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    public decimal Total => Items.Sum(i => i.Subtotal);
}
