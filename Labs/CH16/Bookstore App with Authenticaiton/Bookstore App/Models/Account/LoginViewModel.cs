using System.ComponentModel.DataAnnotations;

namespace Bookstore_App.Models.Account;

public partial class LoginViewModel
{
    [Required(ErrorMessage = "Please enter a UserName.")]
    [StringLength(255)]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a password.")]
    [StringLength(255)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;

    public bool RememberMe { get; set; }

    public string Username
    {
        get => UserName;
        set => UserName = value;
    }
}
