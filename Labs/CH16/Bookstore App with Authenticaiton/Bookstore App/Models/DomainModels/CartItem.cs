namespace Bookstore_App.Models.DomainModels;

public class CartItem
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public Book Book { get; set; } = null!;
}
