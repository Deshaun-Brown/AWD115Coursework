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
            },
            new Product
            {
                Name = "Kettlebell 20lb",
                Description = "Cast iron kettlebell for strength training",
                ImageUrl = "/images/kettlebell.jpg",
                Price = 49.99m,
                Quantity = 40,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Jump Rope",
                Description = "Adjustable speed jump rope for cardio workouts",
                ImageUrl = "/images/jump-rope.jpg",
                Price = 14.99m,
                Quantity = 120,
                Category = accessories
            },
            new Product
            {
                Name = "Foam Roller",
                Description = "High density foam roller for recovery and mobility",
                ImageUrl = "/images/foam-roller.jpg",
                Price = 22.99m,
                Quantity = 80,
                Category = accessories
            },
            new Product
            {
                Name = "Pull-Up Bar",
                Description = "Doorway pull-up bar for upper body training",
                ImageUrl = "/images/pullup-bar.jpg",
                Price = 34.99m,
                Quantity = 35,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Adjustable Bench",
                Description = "Incline/decline bench for versatile lifting",
                ImageUrl = "/images/bench.jpg",
                Price = 129.99m,
                Quantity = 15,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Creatine Monohydrate",
                Description = "Unflavored creatine monohydrate 300g",
                ImageUrl = "/images/creatine.jpg",
                Price = 24.99m,
                Quantity = 90,
                Category = supplements
            },
            new Product
            {
                Name = "Pre-Workout",
                Description = "Pre-workout energy blend fruit punch 30 servings",
                ImageUrl = "/images/preworkout.jpg",
                Price = 34.99m,
                Quantity = 65,
                Category = supplements
            },
            new Product
            {
                Name = "BCAA Powder",
                Description = "BCAA recovery powder lemon 40 servings",
                ImageUrl = "/images/bcaa.jpg",
                Price = 29.99m,
                Quantity = 70,
                Category = supplements
            },
            new Product
            {
                Name = "Multivitamin",
                Description = "Daily multivitamin 90 tablets",
                ImageUrl = "/images/multivitamin.jpg",
                Price = 18.99m,
                Quantity = 110,
                Category = supplements
            },
            new Product
            {
                Name = "Fish Oil",
                Description = "Omega-3 fish oil 120 softgels",
                ImageUrl = "/images/fish-oil.jpg",
                Price = 16.99m,
                Quantity = 95,
                Category = supplements
            },
            new Product
            {
                Name = "Exercise Gloves",
                Description = "Workout gloves with wrist support",
                ImageUrl = "/images/gloves.jpg",
                Price = 17.99m,
                Quantity = 85,
                Category = accessories
            },
            new Product
            {
                Name = "Lifting Straps",
                Description = "Cotton lifting straps for better grip",
                ImageUrl = "/images/lifting-straps.jpg",
                Price = 9.99m,
                Quantity = 140,
                Category = accessories
            },
            new Product
            {
                Name = "Ab Wheel",
                Description = "Abdominal roller wheel for core training",
                ImageUrl = "/images/ab-wheel.jpg",
                Price = 12.99m,
                Quantity = 100,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Medicine Ball 10lb",
                Description = "Textured medicine ball for functional workouts",
                ImageUrl = "/images/medicine-ball.jpg",
                Price = 39.99m,
                Quantity = 30,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Push-Up Handles",
                Description = "Ergonomic push-up handles for wrist comfort",
                ImageUrl = "/images/pushup-handles.jpg",
                Price = 14.49m,
                Quantity = 90,
                Category = accessories
            },
            new Product
            {
                Name = "Balance Ball",
                Description = "Anti-burst stability ball for core training",
                ImageUrl = "/images/balance-ball.jpg",
                Price = 21.99m,
                Quantity = 55,
                Category = fitnessEquipment
            },
            new Product
            {
                Name = "Ankle Weights",
                Description = "Pair of adjustable ankle weights",
                ImageUrl = "/images/ankle-weights.jpg",
                Price = 19.49m,
                Quantity = 60,
                Category = accessories
            },
            new Product
            {
                Name = "Workout Towel",
                Description = "Quick-dry microfiber gym towel",
                ImageUrl = "/images/towel.jpg",
                Price = 11.99m,
                Quantity = 200,
                Category = accessories
            },
            new Product
            {
                Name = "Electrolyte Mix",
                Description = "Electrolyte hydration mix 20 sticks",
                ImageUrl = "/images/electrolytes.jpg",
                Price = 15.99m,
                Quantity = 130,
                Category = supplements
            },
            new Product
            {
                Name = "Protein Bars",
                Description = "Box of 12 high-protein bars",
                ImageUrl = "/images/protein-bars.jpg",
                Price = 27.99m,
                Quantity = 75,
                Category = supplements
            },
            new Product
            {
                Name = "Gym Bag",
                Description = "Durable duffel gym bag with shoe compartment",
                ImageUrl = "/images/gym-bag.jpg",
                Price = 34.49m,
                Quantity = 45,
                Category = accessories
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
