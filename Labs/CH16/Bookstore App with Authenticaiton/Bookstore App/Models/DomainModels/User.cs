using Microsoft.AspNetCore.Identity;

namespace Bookstore_App.Models.DomainModels;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
}
