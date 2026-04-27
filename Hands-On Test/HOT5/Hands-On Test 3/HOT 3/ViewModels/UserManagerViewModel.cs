namespace Pharmaceuticals.ViewModels;

public class UserManagerViewModel
{
    public bool AdminRoleExists { get; set; }
    public List<UserManagerUserItemViewModel> Users { get; set; } = new();
}

public class UserManagerUserItemViewModel
{
    public string Id { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsAdmin { get; set; }
}
