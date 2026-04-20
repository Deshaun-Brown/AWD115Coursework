using Bookstore_App.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Models.DataLayer.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Isbn).IsRequired().HasMaxLength(20);
        builder.Property(b => b.Price).HasPrecision(10, 2);

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Book { Id = 1, Title = "1984", Isbn = "9780451524935", Price = 14.99m, PublishDate = new DateOnly(1949, 6, 8), GenreId = 1, AuthorId = 1 },
            new Book { Id = 2, Title = "Sapiens", Isbn = "9780062316097", Price = 19.99m, PublishDate = new DateOnly(2011, 1, 1), GenreId = 2, AuthorId = 2 },
            new Book { Id = 3, Title = "The Pragmatic Programmer", Isbn = "9780135957059", Price = 44.99m, PublishDate = new DateOnly(2019, 9, 13), GenreId = 3, AuthorId = 3 },
            new Book { Id = 4, AuthorId = 1, GenreId = 1, Isbn = "9780385537030", Price = 14.99m, PublishDate = new DateOnly(2011, 9, 13), Title = "The Night Circus" },
            new Book { Id = 5, AuthorId = 2, GenreId = 2, Isbn = "9780593135204", Price = 18.99m, PublishDate = new DateOnly(2021, 5, 4), Title = "Project Hail Mary" },
            new Book { Id = 6, AuthorId = 3, GenreId = 3, Isbn = "9781250301697", Price = 13.99m, PublishDate = new DateOnly(2019, 5, 1), Title = "The Silent Patient" },
            new Book { Id = 7, AuthorId = 1, GenreId = 1, Isbn = "9780393592389", Price = 15.99m, PublishDate = new DateOnly(2018, 2, 20), Title = "Educated" },
            new Book { Id = 8, AuthorId = 2, GenreId = 2, Isbn = "9780316341370", Price = 12.99m, PublishDate = new DateOnly(2018, 4, 10), Title = "Circe" },
            new Book { Id = 9, AuthorId = 3, GenreId = 3, Isbn = "9780553418026", Price = 11.99m, PublishDate = new DateOnly(2014, 2, 11), Title = "The Martian" },
            new Book { Id = 10, AuthorId = 1, GenreId = 1, Isbn = "9780071767903", Price = 12.49m, PublishDate = new DateOnly(2011, 8, 30), Title = "The Song of Achilles" },
            new Book { Id = 11, AuthorId = 2, GenreId = 2, Isbn = "9780735211292", Price = 16.99m, PublishDate = new DateOnly(2018, 10, 16), Title = "Atomic Habits" },
            new Book { Id = 12, AuthorId = 3, GenreId = 3, Isbn = "9780735219090", Price = 10.99m, PublishDate = new DateOnly(2018, 8, 14), Title = "Where the Crawdads Sing" },
            new Book { Id = 13, AuthorId = 1, GenreId = 1, Isbn = "9780525559474", Price = 14.49m, PublishDate = new DateOnly(2020, 8, 13), Title = "The Midnight Library" },
            new Book { Id = 14, AuthorId = 2, GenreId = 2, Isbn = "9780143034902", Price = 13.49m, PublishDate = new DateOnly(2016, 9, 6), Title = "A Gentleman in Moscow" },
            new Book { Id = 15, AuthorId = 3, GenreId = 3, Isbn = "9780525479988", Price = 15.49m, PublishDate = new DateOnly(2020, 6, 2), Title = "The Vanishing Half" },
            new Book { Id = 16, AuthorId = 1, GenreId = 1, Isbn = "9780743273565", Price = 10.99m, PublishDate = new DateOnly(1925, 4, 10), Title = "The Great Gatsby" },
            new Book { Id = 17, AuthorId = 2, GenreId = 2, Isbn = "9780061120084", Price = 7.99m, PublishDate = new DateOnly(1960, 7, 11), Title = "To Kill a Mockingbird" },
            new Book { Id = 18, AuthorId = 3, GenreId = 3, Isbn = "9781485550464", Price = 9.99m, PublishDate = new DateOnly(1813, 1, 28), Title = "Pride and Prejudice" },
            new Book { Id = 19, AuthorId = 1, GenreId = 1, Isbn = "9780316769488", Price = 10.99m, PublishDate = new DateOnly(1951, 7, 16), Title = "The Catcher in the Rye" },
            new Book { Id = 20, AuthorId = 2, GenreId = 2, Isbn = "9780439708180", Price = 10.99m, PublishDate = new DateOnly(1997, 9, 1), Title = "Harry Potter and the Sorcerer's Stone" },
            new Book { Id = 21, AuthorId = 3, GenreId = 3, Isbn = "9780547928227", Price = 10.99m, PublishDate = new DateOnly(1937, 9, 21), Title = "The Hobbit" },
            new Book { Id = 22, AuthorId = 1, GenreId = 1, Isbn = "9780307474278", Price = 10.99m, PublishDate = new DateOnly(2003, 3, 18), Title = "The Da Vinci Code" },
            new Book { Id = 23, Title = "The Alchemist", Isbn = "9780062315007", Price = 10.99m, PublishDate = new DateOnly(1988, 4, 15), GenreId = 8, AuthorId = 23 },
            new Book { Id = 24, Title = "The Angel's Game", Isbn = "9781933372399", Price = 10.99m, PublishDate = new DateOnly(2008, 4, 17), GenreId = 9, AuthorId = 24 },
            new Book { Id = 25, Title = "The Shadow of the Wind", Isbn = "9781944910034", Price = 10.99m, PublishDate = new DateOnly(2001, 4, 17), GenreId = 10, AuthorId = 25 },
            new Book { Id = 26, Title = "Norwegian Wood", Isbn = "9789603649662", Price = 10.99m, PublishDate = new DateOnly(1987, 9, 4), GenreId = 11, AuthorId = 26 },
            new Book { Id = 27, Title = "The Wind-Up Bird Chronicle", Isbn = "9780679775430", Price = 10.99m, PublishDate = new DateOnly(1994, 9, 1), GenreId = 12, AuthorId = 27 },
            new Book { Id = 28, Title = "Kafka on the Shore", Isbn = "9781400079278", Price = 10.99m, PublishDate = new DateOnly(2002, 9, 12), GenreId = 13, AuthorId = 28 },
            new Book { Id = 29, Title = "1Q84", Isbn = "9780307706870", Price = 10.99m, PublishDate = new DateOnly(2009, 4, 16), GenreId = 14, AuthorId = 29 },
            new Book { Id = 30, Title = "The Brief Wondrous Life of Oscar Wao", Isbn = "9781594483295", Price = 10.99m, PublishDate = new DateOnly(2007, 9, 6), GenreId = 15, AuthorId = 30 }
        );
    }
}
