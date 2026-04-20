using System.ComponentModel.DataAnnotations;

namespace Bookstore_App.Models.DomainModels;

public class Author
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Bio { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();

    public string FullName => $"{FirstName} {LastName}";
}
