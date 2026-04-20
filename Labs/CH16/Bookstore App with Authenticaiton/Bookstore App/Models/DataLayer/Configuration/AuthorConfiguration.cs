using Bookstore_App.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Models.DataLayer.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);

        builder.HasData(
            new Author { Id = 1, FirstName = "George", LastName = "Orwell", Bio = "English novelist and essayist." },
            new Author { Id = 2, FirstName = "Yuval", LastName = "Harari", Bio = "Historian and author." },
            new Author { Id = 3, FirstName = "Andrew", LastName = "Hunt", Bio = "Co-author of The Pragmatic Programmer." });
    }
}
