using Bookstore_App.Models.DomainModels;

namespace Bookstore_App.Models.ViewModels;

public class BookListViewModel
{
    public IEnumerable<Book> Books { get; set; } = null!;
}
