using System.ComponentModel.DataAnnotations;

namespace Bookstore_App.Models.ViewModels;

public partial class RegisterViewModel
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;

    public bool IsAdmin { get; set; }
    public string UserName { get; internal set; }

    public string Username
    {
        get => UserName;
        set => UserName = value;
    }
}
