using Aircraft_Parts_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Aircraft_Parts_App.Data
{
    public class PartContext(DbContextOptions<PartContext> options) : DbContext(options)
    {
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartDetails> PartDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PartCategory> PartCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure decimal precision for UnitPrice
            modelBuilder.Entity<PartDetails>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);  // 18 total digits, 2 after decimal point

            // Seed Suppliers (One-To-Many)
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, SupplierName = "Boeing Supply Chain", ContactEmail = "supply@boeing.com", PhoneNumber = "1-800-BOEING-1" },
                new Supplier { SupplierId = 2, SupplierName = "Pratt & Whitney Distribution", ContactEmail = "parts@pw.com", PhoneNumber = "1-800-PW-PARTS" },
                new Supplier { SupplierId = 3, SupplierName = "Lockheed Parts Direct", ContactEmail = "orders@lockheed.com", PhoneNumber = "1-800-LM-PARTS" },
                new Supplier { SupplierId = 4, SupplierName = "Aerospace Components Inc", ContactEmail = "sales@aerocomp.com", PhoneNumber = "1-888-AERO-123" }
            );

            // Seed Categories (Many-To-Many)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Avionics", Description = "Electronic systems and components" },
                new Category { CategoryId = 2, CategoryName = "Propulsion", Description = "Engines and related systems" },
                new Category { CategoryId = 3, CategoryName = "Structural", Description = "Airframe and structural components" },
                new Category { CategoryId = 4, CategoryName = "Hydraulics", Description = "Hydraulic systems and components" },
                new Category { CategoryId = 5, CategoryName = "Fuel Systems", Description = "Fuel delivery and storage" }
            );

            // Seed Parts (with SupplierId foreign key)
            modelBuilder.Entity<Part>().HasData(
                new Part { Id = 1, NIIN = "123456789", PartName = "Sonar", Manufacturer = "Boeing", QuantityInStock = 12456, SupplierId = 1 },
                new Part { Id = 2, NIIN = "987654321", PartName = "Turbine Engine", Manufacturer = "Pratt & Whitney", QuantityInStock = 8420, SupplierId = 2 },
                new Part { Id = 3, NIIN = "456789123", PartName = "Landing Gear Assembly", Manufacturer = "Lockheed Martin", QuantityInStock = 3200, SupplierId = 3 },
                new Part { Id = 4, NIIN = "789123456", PartName = "Avionics Control Unit", Manufacturer = "Northrop Grumman", QuantityInStock = 15670, SupplierId = 4 },
                new Part { Id = 5, NIIN = "321654987", PartName = "Hydraulic Pump", Manufacturer = "Parker Aerospace", QuantityInStock = 9850, SupplierId = 4 },
                new Part { Id = 6, NIIN = "654987321", PartName = "Radar System", Manufacturer = "Raytheon", QuantityInStock = 5430, SupplierId = 1 },
                new Part { Id = 7, NIIN = "147258369", PartName = "Fuel Injection Nozzle", Manufacturer = "General Electric", QuantityInStock = 22100, SupplierId = 2 }
            );

            // Seed PartDetails (One-To-One)
            modelBuilder.Entity<PartDetails>().HasData(
                new PartDetails { PartId = 1, Weight = "45.5 kg", Dimensions = "120x80x60 cm", Material = "Titanium Alloy", Certification = "FAA-PMA", Condition = "New", UnitPrice = 12500.00m, SupplierContact = "boeing.parts@boeing.com" },
                new PartDetails { PartId = 2, Weight = "2850 kg", Dimensions = "280x150x180 cm", Material = "Nickel-based Superalloy", Certification = "FAA-TSO", Condition = "New", UnitPrice = 450000.00m, SupplierContact = "service@pwc.com" },
                new PartDetails { PartId = 3, Weight = "320 kg", Dimensions = "200x100x150 cm", Material = "Steel Alloy", Certification = "FAA-PMA", Condition = "Overhauled", UnitPrice = 85000.00m, SupplierContact = "parts@lockheedmartin.com" },
                new PartDetails { PartId = 4, Weight = "12.8 kg", Dimensions = "45x35x20 cm", Material = "Aluminum Composite", Certification = "DO-160", Condition = "New", UnitPrice = 28500.00m, SupplierContact = "avionics@northropgrumman.com" },
                new PartDetails { PartId = 5, Weight = "18.5 kg", Dimensions = "60x40x35 cm", Material = "Stainless Steel", Certification = "FAA-PMA", Condition = "New", UnitPrice = 6750.00m, SupplierContact = "sales@parker.com" },
                new PartDetails { PartId = 6, Weight = "95 kg", Dimensions = "150x80x70 cm", Material = "Composite Materials", Certification = "MIL-STD-461", Condition = "New", UnitPrice = 175000.00m, SupplierContact = "defense@raytheon.com" },
                new PartDetails { PartId = 7, Weight = "2.3 kg", Dimensions = "25x15x10 cm", Material = "Ceramic Composite", Certification = "FAA-TSO", Condition = "New", UnitPrice = 3200.00m, SupplierContact = "aviation@ge.com" }
            );

            // Seed PartCategory (Join table for Many-To-Many)
            modelBuilder.Entity<PartCategory>().HasData(
                // Sonar - Avionics
                new PartCategory { PartCategoryId = 1, PartId = 1, CategoryId = 1 },
                // Turbine Engine - Propulsion
                new PartCategory { PartCategoryId = 2, PartId = 2, CategoryId = 2 },
                new PartCategory { PartCategoryId = 3, PartId = 2, CategoryId = 5 }, // Also Fuel Systems
                // Landing Gear - Structural, Hydraulics
                new PartCategory { PartCategoryId = 4, PartId = 3, CategoryId = 3 },
                new PartCategory { PartCategoryId = 5, PartId = 3, CategoryId = 4 },
                // Avionics Control - Avionics
                new PartCategory { PartCategoryId = 6, PartId = 4, CategoryId = 1 },
                // Hydraulic Pump - Hydraulics
                new PartCategory { PartCategoryId = 7, PartId = 5, CategoryId = 4 },
                // Radar - Avionics
                new PartCategory { PartCategoryId = 8, PartId = 6, CategoryId = 1 },
                // Fuel Nozzle - Fuel Systems, Propulsion
                new PartCategory { PartCategoryId = 9, PartId = 7, CategoryId = 5 },
                new PartCategory { PartCategoryId = 10, PartId = 7, CategoryId = 2 }
            );
        }
    }
}