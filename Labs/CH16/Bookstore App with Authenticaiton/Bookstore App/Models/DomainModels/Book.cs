using System.ComponentModel.DataAnnotations;

namespace Bookstore_App.Models.DomainModels;

public class Book
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Range(0.01, 9999.99)]
    public decimal Price { get; set; }

    [DataType(DataType.Date)]
    public DateOnly PublishDate { get; set; }

    public int GenreId { get; set; }
    public int AuthorId { get; set; }

    public Genre Genre { get; set; } = null!;
    public Author Author { get; set; } = null!;
}
