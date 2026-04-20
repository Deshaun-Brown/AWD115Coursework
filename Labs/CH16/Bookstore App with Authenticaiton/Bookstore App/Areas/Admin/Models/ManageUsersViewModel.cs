namespace Bookstore_App.Areas.Admin.Models;

public class ManageUsersViewModel
{
    public IList<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    public IList<string> Roles { get; set; } = new List<string>();
}
