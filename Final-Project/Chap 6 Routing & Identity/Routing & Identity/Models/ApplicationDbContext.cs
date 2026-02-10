using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Fitness Equipment" },
            new Category { CategoryId = 2, Name = "Supplements" },
            new Category { CategoryId = 3, Name = "Accessories" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                Name = "Yoga Mat",
                Description = "Non-slip yoga mat for fitness exercises",
                ImageUrl = "/images/yoga-mat.jpg",
                Price = 29.99m,
                Quantity = 50,
                CategoryId = 1
            },
            new Product
            {
                ProductId = 2,
                Name = "Dumbbells Set",
                Description = "Adjustable dumbbells 5-50 lbs",
                ImageUrl = "/images/dumbbells.jpg",
                Price = 199.99m,
                Quantity = 25,
                CategoryId = 1
            },
            new Product
            {
                ProductId = 3,
                Name = "Resistance Bands",
                Description = "Set of 5 resistance bands with different levels",
                ImageUrl = "/images/resistance-bands.jpg",
                Price = 24.99m,
                Quantity = 100,
                CategoryId = 1
            },
            new Product
            {
                ProductId = 4,
                Name = "Protein Powder",
                Description = "Whey protein powder chocolate flavor 2lb",
                ImageUrl = "/images/protein.jpg",
                Price = 39.99m,
                Quantity = 75,
                CategoryId = 2
            },
            new Product
            {
                ProductId = 5,
                Name = "Water Bottle",
                Description = "Insulated stainless steel water bottle 32oz",
                ImageUrl = "/images/water-bottle.jpg",
                Price = 19.99m,
                Quantity = 150,
                CategoryId = 3
            }
        );
    }
}