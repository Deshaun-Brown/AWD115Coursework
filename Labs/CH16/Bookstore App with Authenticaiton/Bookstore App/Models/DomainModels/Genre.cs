using System.ComponentModel.DataAnnotations;

namespace Bookstore_App.Models.DomainModels;

public class Genre
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
