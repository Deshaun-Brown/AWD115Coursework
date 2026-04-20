using Bookstore_App.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore_App.Models.DataLayer.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name).IsRequired().HasMaxLength(80);

        builder.HasData(
            new Genre { Id = 1, Name = "Fiction", Description = "Stories driven by imagination." },
            new Genre { Id = 2, Name = "Non-Fiction", Description = "Fact-based books." },
            new Genre { Id = 3, Name = "Technology", Description = "Programming and software engineering titles." });
    }
}
