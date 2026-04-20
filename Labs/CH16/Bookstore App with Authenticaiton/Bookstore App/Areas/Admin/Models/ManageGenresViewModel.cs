using Bookstore_App.Models.DomainModels;

namespace Bookstore_App.Areas.Admin.Models;

public class ManageGenresViewModel
{
    public IList<Genre> Genres { get; set; } = new List<Genre>();
}
