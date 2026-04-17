using Pharmaceuticals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Pharmaceuticals.Data;

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
                var adminEmail = config?["AdminUser:Email"] ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@local.test";
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
            new Category { Name = "Prescription" },
            new Category { Name = "Over-the-Counter" },
            new Category { Name = "Vitamins & Supplements" }
        };

        var prescription = categories[0];
        var otc = categories[1];
        var vitamins = categories[2];

        var products = new[]
        {
            new Product
            {
                Name = "Amoxicillin",
                Description = "Antibiotic used to treat a number of bacterial infections.",
                ImageUrl = "/images/amoxicillin.jpg",
                Price = 14.99m,
                Quantity = 50,
                Category = prescription
            },
            new Product
            {
                Name = "Ibuprofen",
                Description = "Pain reliever and fever reducer.",
                ImageUrl = "/wwwroot/images/coughing-young-man-on-light-background-2BFADBK.jpg",
                Price = 8.99m,
                Quantity = 200,
                Category = otc
            },
            new Product
            {
                Name = "Vitamin C 1000mg",
                Description = "Daily vitamin C supplement for immune support.",
                ImageUrl = "/images/20250412_033020761_iOS.jpg",
                Price = 12.99m,
                Quantity = 100,
                Category = vitamins
            },
            new Product
            {
                Name = "Lisinopril",
                Description = "Medication used to treat high blood pressure.",
                ImageUrl = "/images/coughing-young-man-on-light-600w-1704402550.jpg",
                Price = 24.99m,
                Quantity = 75,
                Category = prescription
            },
            new Product
            {
                Name = "Acetaminophen",
                Description = "Pain reliever and fever reducer.",
                ImageUrl = "/wwwroot/images/Pills.jpg",
                Price = 7.99m,
                Quantity = 150,
                Category = otc
            },
            new Product
            {
                Name = "Omega-3 Fish Oil",
                Description = "Supports heart, brain, and joint health.",
                ImageUrl = "/images/hero-fitness.jpg",
                Price = 19.99m,
                Quantity = 120,
                Category = vitamins
            },
            new Product
            {
                Name = "Metformin",
                Description = "Used to treat type 2 diabetes.",
                ImageUrl = "/images/coughing-young-man-on-light-background-2BFADBK.jpg",
                Price = 18.99m,
                Quantity = 90,
                Category = prescription
            },
            new Product
            {
                Name = "Antacid Chews",
                Description = "Fast relief of heartburn and acid indigestion.",
                ImageUrl = "/wwwroot/images/coughing.jpg",
                Price = 6.49m,
                Quantity = 110,
                Category = otc
            },
            new Product
            {
                Name = "Vitamin D3 5000 IU",
                Description = "Supports bone and immune health.",
                ImageUrl = "/wwwroot/images/20250412_033020761_iOS.jpg",
                Price = 14.49m,
                Quantity = 140,
                Category = vitamins
            },
            new Product
            {
                Name = "Atorvastatin",
                Description = "Statin medication to treat high cholesterol.",
                ImageUrl = "/wwwroot/images/coughing-young-man-on-light-background-2BFADBK.jpg",
                Price = 29.99m,
                Quantity = 60,
                Category = prescription
            },
            new Product
            {
                Name = "Cough Syrup",
                Description = "Soothes cough and throat irritation.",
                ImageUrl = "/images/coughing.jpg",
                Price = 10.99m,
                Quantity = 85,
                Category = otc
            },
            new Product
            {
                Name = "Multivitamin for Men",
                Description = "Comprehensive daily nutrition for men.",
                ImageUrl = "/images/hero-fitness.jpg",
                Price = 22.99m,
                Quantity = 90,
                Category = vitamins
            },
            new Product
            {
                Name = "Multivitamin for Women",
                Description = "Comprehensive daily nutrition for women.",
                ImageUrl = "/images/hero-fitness.jpg",
                Price = 22.99m,
                Quantity = 90,
                Category = vitamins
            },
            new Product
            {
                Name = "Levothyroxine",
                Description = "Treats hypothyroidism (underactive thyroid).",
                ImageUrl = "/images/the-woman-coughed-and-covered-her-mouth-with-her-hand-and-sat-on-the-bed-free-photo.jpg",
                Price = 16.99m,
                Quantity = 100,
                Category = prescription
            },
            new Product
            {
                Name = "Loratadine",
                Description = "Non-drowsy allergy relief 24-hour.",
                ImageUrl = "/images/Pills.jpg",
                Price = 15.99m,
                Quantity = 70,
                Category = otc
            },
            new Product
            {
                Name = "Melatonin 5mg",
                Description = "Supports restful sleep.",
                ImageUrl = "/images/coughing-young-man-on-light-background-2BFADBK.jpg",
                Price = 9.99m,
                Quantity = 130,
                Category = vitamins
            },
            new Product
            {
                Name = "Albuterol Inhaler",
                Description = "Bronchodilator for asthma relief.",
                ImageUrl = "/images/coughing.jpg",
                Price = 45.99m,
                Quantity = 40,
                Category = prescription
            },
            new Product
            {
                Name = "Hydrocortisone Cream",
                Description = "Itch and rash relief anti-itch cream.",
                ImageUrl = "/images/20250412_033020761_iOS.jpg",
                Price = 8.49m,
                Quantity = 95,
                Category = otc
            },
            new Product
            {
                Name = "Zinc 50mg",
                Description = "Supports immune system function.",
                ImageUrl = "/images/Pills.jpg",
                Price = 11.99m,
                Quantity = 115,
                Category = vitamins
            },
            new Product
            {
                Name = "Probiotics 50 Billion CFU",
                Description = "Digestive and immune support.",
                ImageUrl = "/images/hero-fitness.jpg",
                Price = 28.99m,
                Quantity = 65,
                Category = vitamins
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
