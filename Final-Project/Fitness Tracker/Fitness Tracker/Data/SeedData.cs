using Fitness_Tracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Fitness_Tracker.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        // Ensure roles and a test admin user exist even if products are already seeded.
        try
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var config = serviceProvider.GetService<IConfiguration>();

            if (roleManager != null && userManager != null)
            {
                const string adminRole = "Admin";

                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                // Read admin credentials from configuration or environment variables
                // Default admin for coursework
                var adminEmail = config?["AdminUser:Email"] ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "evangdmestad@insideranken.org";
                var adminPassword = config?["AdminUser:Password"] ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "Admin123!";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                    else
                    {
                        // ignore failures here - migrations/seed should not throw; consider logging in future
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                }
            }
        }
        catch
        {
            // swallow exceptions to avoid blocking migrations; consider logging in real apps
        }

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
                Price = 19.99m,
                Quantity = 110,
                Category = supplements
            }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
