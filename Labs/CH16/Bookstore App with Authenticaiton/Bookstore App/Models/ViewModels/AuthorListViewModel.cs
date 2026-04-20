using Bookstore_App.Models.DomainModels;

namespace Bookstore_App.Models.ViewModels;

public class AuthorListViewModel
{
    public IEnumerable<Author> Authors { get; set; } = null!;
}
