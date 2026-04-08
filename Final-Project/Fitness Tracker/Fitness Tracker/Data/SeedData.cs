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
            new Category { Name = "Cardiovascular" },
            new Category { Name = "Respiratory" },
            new Category { Name = "Gastrointestinal" },
            new Category { Name = "CNS / Mental Health" },
            new Category { Name = "Endocrine" },
            new Category { Name = "Antibiotics & Antifungals" },
            new Category { Name = "Miscellaneous" }
        };

        var cardio = categories[0];
        var resp = categories[1];
        var gi = categories[2];
        var cns = categories[3];
        var endo = categories[4];
        var anti = categories[5];
        var misc = categories[6];

        var products = new[]
        {
            // Cardiovascular
            new Product { Name = "Lisinopril", Description = "ACE inhibitor - hypertension", Price = 10.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Amlodipine", Description = "Calcium channel blocker - hypertension", Price = 12.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Losartan", Description = "ARB - hypertension", Price = 15.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Metoprolol tartrate", Description = "Beta-blocker - hypertension", Price = 14.50m, Quantity = 100, Category = cardio },
            new Product { Name = "Metoprolol succinate", Description = "Beta-blocker - hypertension/CHF", Price = 18.00m, Quantity = 100, Category = cardio },
            new Product { Name = "Atenolol", Description = "Beta-blocker - hypertension", Price = 9.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Hydrochlorothiazide (HCTZ)", Description = "Diuretic - hypertension", Price = 8.50m, Quantity = 100, Category = cardio },
            new Product { Name = "Furosemide", Description = "Loop diuretic - edema/CHF", Price = 7.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Spironolactone", Description = "Potassium-sparing diuretic - CHF", Price = 19.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Clonidine", Description = "Alpha-agonist - hypertension", Price = 11.20m, Quantity = 100, Category = cardio },
            new Product { Name = "Valsartan", Description = "ARB - hypertension", Price = 16.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Enalapril", Description = "ACE inhibitor - hypertension", Price = 12.50m, Quantity = 100, Category = cardio },
            new Product { Name = "Propranolol", Description = "Beta-blocker - hypertension/migraine", Price = 14.00m, Quantity = 100, Category = cardio },
            new Product { Name = "Diltiazem", Description = "Calcium channel blocker - hypertension", Price = 15.50m, Quantity = 100, Category = cardio },
            new Product { Name = "Verapamil", Description = "Calcium channel blocker - hypertension", Price = 13.99m, Quantity = 100, Category = cardio },
            new Product { Name = "Atorvastatin", Description = "Statin - hyperlipidemia", Price = 25.00m, Quantity = 100, Category = cardio },
            new Product { Name = "Simvastatin", Description = "Statin - hyperlipidemia", Price = 18.50m, Quantity = 100, Category = cardio },
            new Product { Name = "Rosuvastatin", Description = "Statin - hyperlipidemia", Price = 28.00m, Quantity = 100, Category = cardio },
            new Product { Name = "Pravastatin", Description = "Statin - hyperlipidemia", Price = 22.00m, Quantity = 100, Category = cardio },
            new Product { Name = "Warfarin", Description = "Anticoagulant - clot prevention", Price = 10.50m, Quantity = 100, Category = cardio },

            // Respiratory
            new Product { Name = "Albuterol", Description = "Bronchodilator - asthma", Price = 20.00m, Quantity = 100, Category = resp },
            new Product { Name = "Fluticasone (Flonase)", Description = "Corticosteroid - allergies", Price = 22.99m, Quantity = 100, Category = resp },
            new Product { Name = "Montelukast", Description = "Leukotriene inhibitor - asthma/allergies", Price = 15.50m, Quantity = 100, Category = resp },
            new Product { Name = "Tiotropium", Description = "Anticholinergic - COPD", Price = 45.00m, Quantity = 100, Category = resp },
            new Product { Name = "Budesonide/Formoterol (Symbicort)", Description = "ICS/LABA - asthma/COPD", Price = 80.00m, Quantity = 100, Category = resp },
            new Product { Name = "Fluticasone/Salmeterol (Advair)", Description = "ICS/LABA - asthma/COPD", Price = 85.00m, Quantity = 100, Category = resp },
            new Product { Name = "Ipratropium", Description = "Anticholinergic - COPD", Price = 18.00m, Quantity = 100, Category = resp },
            new Product { Name = "Cetirizine", Description = "Antihistamine - allergies", Price = 12.00m, Quantity = 100, Category = resp },
            new Product { Name = "Loratadine", Description = "Antihistamine - allergies", Price = 10.50m, Quantity = 100, Category = resp },
            new Product { Name = "Fexofenadine", Description = "Antihistamine - allergies", Price = 14.50m, Quantity = 100, Category = resp },
            new Product { Name = "Diphenhydramine", Description = "Antihistamine - allergies", Price = 8.99m, Quantity = 100, Category = resp },
            new Product { Name = "Prednisone", Description = "Corticosteroid - inflammation", Price = 6.50m, Quantity = 100, Category = resp },
            new Product { Name = "Methylprednisolone", Description = "Corticosteroid - inflammation", Price = 15.00m, Quantity = 100, Category = resp },
            new Product { Name = "Benzonatate", Description = "Antitussive - cough", Price = 19.99m, Quantity = 100, Category = resp },
            new Product { Name = "Guaifenesin", Description = "Expectorant - cough", Price = 10.00m, Quantity = 100, Category = resp },
            new Product { Name = "Azelastine", Description = "Antihistamine - allergies", Price = 25.00m, Quantity = 100, Category = resp },
            new Product { Name = "Theophylline", Description = "Bronchodilator - asthma", Price = 30.00m, Quantity = 100, Category = resp },
            new Product { Name = "Beclomethasone", Description = "ICS - asthma", Price = 40.00m, Quantity = 100, Category = resp },
            new Product { Name = "Mometasone", Description = "ICS - allergies", Price = 35.00m, Quantity = 100, Category = resp },
            new Product { Name = "Umeclidinium/Vilanterol", Description = "LAMA/LABA - COPD", Price = 90.00m, Quantity = 100, Category = resp },

            // Gastrointestinal
            new Product { Name = "Omeprazole", Description = "PPI - GERD", Price = 14.99m, Quantity = 100, Category = gi },
            new Product { Name = "Pantoprazole", Description = "PPI - GERD", Price = 16.50m, Quantity = 100, Category = gi },
            new Product { Name = "Esomeprazole", Description = "PPI - GERD", Price = 18.99m, Quantity = 100, Category = gi },
            new Product { Name = "Lansoprazole", Description = "PPI - GERD", Price = 15.00m, Quantity = 100, Category = gi },
            new Product { Name = "Ranitidine", Description = "H2 blocker - GERD", Price = 9.99m, Quantity = 100, Category = gi },
            new Product { Name = "Famotidine", Description = "H2 blocker - GERD", Price = 11.50m, Quantity = 100, Category = gi },
            new Product { Name = "Ondansetron", Description = "Antiemetic - nausea", Price = 21.00m, Quantity = 100, Category = gi },
            new Product { Name = "Metoclopramide", Description = "Prokinetic - nausea/GERD", Price = 14.00m, Quantity = 100, Category = gi },
            new Product { Name = "Dicyclomine", Description = "Antispasmodic - IBS", Price = 13.99m, Quantity = 100, Category = gi },
            new Product { Name = "Loperamide", Description = "Antidiarrheal", Price = 8.50m, Quantity = 100, Category = gi },
            new Product { Name = "Sucralfate", Description = "Ulcer protectant", Price = 20.00m, Quantity = 100, Category = gi },
            new Product { Name = "Mesalamine", Description = "Anti-inflammatory - ulcerative colitis", Price = 45.00m, Quantity = 100, Category = gi },
            new Product { Name = "Polyethylene glycol (PEG)", Description = "Laxative", Price = 12.00m, Quantity = 100, Category = gi },
            new Product { Name = "Docusate sodium", Description = "Stool softener", Price = 7.50m, Quantity = 100, Category = gi },
            new Product { Name = "Bisacodyl", Description = "Stimulant laxative", Price = 6.99m, Quantity = 100, Category = gi },
            new Product { Name = "Promethazine", Description = "Antiemetic/antihistamine", Price = 15.50m, Quantity = 100, Category = gi },
            new Product { Name = "Prochlorperazine", Description = "Antiemetic", Price = 18.00m, Quantity = 100, Category = gi },
            new Product { Name = "Simethicone", Description = "Antiflatulent", Price = 9.00m, Quantity = 100, Category = gi },
            new Product { Name = "Magnesium hydroxide", Description = "Antacid/laxative", Price = 8.00m, Quantity = 100, Category = gi },
            new Product { Name = "Bismuth subsalicylate", Description = "Antidiarrheal", Price = 10.50m, Quantity = 100, Category = gi },

            // CNS / Mental Health
            new Product { Name = "Sertraline", Description = "SSRI - depression/anxiety", Price = 12.99m, Quantity = 100, Category = cns },
            new Product { Name = "Fluoxetine", Description = "SSRI - depression", Price = 11.50m, Quantity = 100, Category = cns },
            new Product { Name = "Escitalopram", Description = "SSRI - depression/anxiety", Price = 14.50m, Quantity = 100, Category = cns },
            new Product { Name = "Citalopram", Description = "SSRI - depression", Price = 10.99m, Quantity = 100, Category = cns },
            new Product { Name = "Paroxetine", Description = "SSRI - depression/anxiety", Price = 13.99m, Quantity = 100, Category = cns },
            new Product { Name = "Venlafaxine", Description = "SNRI - depression/anxiety", Price = 16.00m, Quantity = 100, Category = cns },
            new Product { Name = "Duloxetine", Description = "SNRI - depression/neuropathy", Price = 19.50m, Quantity = 100, Category = cns },
            new Product { Name = "Amitriptyline", Description = "TCA - depression/neuropathy", Price = 9.50m, Quantity = 100, Category = cns },
            new Product { Name = "Bupropion", Description = "Antidepressant - depression/smoking cessation", Price = 18.00m, Quantity = 100, Category = cns },
            new Product { Name = "Trazodone", Description = "Antidepressant - insomnia", Price = 15.00m, Quantity = 100, Category = cns },
            new Product { Name = "Alprazolam", Description = "Benzodiazepine - anxiety", Price = 25.00m, Quantity = 100, Category = cns },
            new Product { Name = "Lorazepam", Description = "Benzodiazepine - anxiety", Price = 22.00m, Quantity = 100, Category = cns },
            new Product { Name = "Diazepam", Description = "Benzodiazepine - anxiety/muscle spasms", Price = 20.00m, Quantity = 100, Category = cns },
            new Product { Name = "Clonazepam", Description = "Benzodiazepine - seizures/anxiety", Price = 24.50m, Quantity = 100, Category = cns },
            new Product { Name = "Gabapentin", Description = "Anticonvulsant - neuropathy", Price = 18.50m, Quantity = 100, Category = cns },
            new Product { Name = "Pregabalin", Description = "Anticonvulsant - neuropathy", Price = 38.00m, Quantity = 100, Category = cns },
            new Product { Name = "Lamotrigine", Description = "Anticonvulsant - seizures", Price = 29.00m, Quantity = 100, Category = cns },
            new Product { Name = "Levetiracetam", Description = "Anticonvulsant - seizures", Price = 35.50m, Quantity = 100, Category = cns },
            new Product { Name = "Topiramate", Description = "Anticonvulsant - migraines", Price = 28.00m, Quantity = 100, Category = cns },
            new Product { Name = "Risperidone", Description = "Antipsychotic - schizophrenia", Price = 42.00m, Quantity = 100, Category = cns },

            // Endocrine
            new Product { Name = "Metformin", Description = "Biguanide - type 2 diabetes", Price = 8.50m, Quantity = 100, Category = endo },
            new Product { Name = "Glipizide", Description = "Sulfonylurea - diabetes", Price = 10.00m, Quantity = 100, Category = endo },
            new Product { Name = "Insulin glargine", Description = "Long-acting insulin - diabetes", Price = 120.00m, Quantity = 100, Category = endo },
            new Product { Name = "Insulin lispro", Description = "Rapid-acting insulin - diabetes", Price = 135.00m, Quantity = 100, Category = endo },
            new Product { Name = "Levothyroxine", Description = "Thyroid hormone - hypothyroidism", Price = 9.99m, Quantity = 100, Category = endo },
            new Product { Name = "Liothyronine", Description = "Thyroid hormone", Price = 25.00m, Quantity = 100, Category = endo },

            // Antibiotics
            new Product { Name = "Amoxicillin", Description = "Penicillin antibiotic", Price = 12.00m, Quantity = 100, Category = anti },
            new Product { Name = "Amoxicillin/clavulanate", Description = "Penicillin antibiotic", Price = 18.50m, Quantity = 100, Category = anti },
            new Product { Name = "Azithromycin", Description = "Macrolide antibiotic", Price = 22.00m, Quantity = 100, Category = anti },
            new Product { Name = "Clarithromycin", Description = "Macrolide antibiotic", Price = 28.00m, Quantity = 100, Category = anti },
            new Product { Name = "Cephalexin", Description = "Cephalosporin antibiotic", Price = 15.00m, Quantity = 100, Category = anti },
            new Product { Name = "Cefdinir", Description = "Cephalosporin antibiotic", Price = 30.00m, Quantity = 100, Category = anti },
            new Product { Name = "Doxycycline", Description = "Tetracycline antibiotic", Price = 19.99m, Quantity = 100, Category = anti },
            new Product { Name = "Ciprofloxacin", Description = "Fluoroquinolone antibiotic", Price = 24.00m, Quantity = 100, Category = anti },
            new Product { Name = "Levofloxacin", Description = "Fluoroquinolone antibiotic", Price = 26.50m, Quantity = 100, Category = anti },
            new Product { Name = "Clindamycin", Description = "Lincosamide antibiotic", Price = 21.00m, Quantity = 100, Category = anti },
            new Product { Name = "Metronidazole", Description = "Antibiotic/antiprotozoal", Price = 14.50m, Quantity = 100, Category = anti },
            new Product { Name = "Sulfamethoxazole/Trimethoprim (Bactrim)", Description = "Sulfonamide antibiotic", Price = 16.00m, Quantity = 100, Category = anti },
            new Product { Name = "Nitrofurantoin", Description = "Urinary antibiotic", Price = 18.00m, Quantity = 100, Category = anti },
            new Product { Name = "Fluconazole", Description = "Antifungal", Price = 15.99m, Quantity = 100, Category = anti }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
