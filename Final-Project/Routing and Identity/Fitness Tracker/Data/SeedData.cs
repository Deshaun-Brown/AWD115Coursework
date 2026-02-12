using Fitness_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync() || await context.Products.AnyAsync())
            return;

        var categories = new[]
        {
            new Category { Name = "Fitness Equipment" },
            new Category { Name = "Supplements" },
            new Category { Name = "Accessories" }
        };

        var fitnessEquipment = categories[0];
        var supplements = categories[1];
        var accessories = categories[2];

        var products = new[]
        {
            new Product
            {
                Name = "Yoga Mat",
                Description = "Non-slip yoga mat for fitness exercises",
                ImageUrl = "/images/yoga-mat.jpg",
                Price = 29.99m,
                Quantity = 50,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Dumbbells Set",
                Description = "Adjustable dumbbells 5-50 lbs",
                ImageUrl = "/images/dumbbells.jpg",
                Price = 199.99m,
                Quantity = 25,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Resistance Bands",
                Description = "Set of 5 resistance bands with different levels",
                ImageUrl = "/images/resistance-bands.jpg",
                Price = 24.99m,
                Quantity = 100,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Protein Powder",
                Description = "Whey protein powder chocolate flavor 2lb",
                ImageUrl = "/images/protein.jpg",
                Price = 39.99m,
                Quantity = 75,
                Category = supplements
            },
            new Product
            {
                Name = "Water Bottle",
                Description = "Insulated stainless steel water bottle 32oz",
                ImageUrl = "/images/water-bottle.jpg",
                Price = 19.99m,
                Quantity = 150,
                Category = accessories
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
