using Aircraft_Parts_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Aircraft_Parts_App.Data
{
    public class PartContext : DbContext
    {
        public PartContext(DbContextOptions<PartContext> options) : base(options)
        {
        }
        public DbSet<AircraftPart> Parts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AircraftPart>().HasData(
                new AircraftPart { Id = 1, NIIN = "123456789", PartName = "Sonar", Manufacturer = "Boeing", QuantityInStock = 12456 },
                new AircraftPart { Id = 2, NIIN = "987654321", PartName = "Turbine Engine", Manufacturer = "Pratt & Whitney", QuantityInStock = 8420 },
                new AircraftPart { Id = 3, NIIN = "456789123", PartName = "Landing Gear Assembly", Manufacturer = "Lockheed Martin", QuantityInStock = 3200 },
                new AircraftPart { Id = 4, NIIN = "789123456", PartName = "Avionics Control Unit", Manufacturer = "Northrop Grumman", QuantityInStock = 15670 },
                new AircraftPart { Id = 5, NIIN = "321654987", PartName = "Hydraulic Pump", Manufacturer = "Parker Aerospace", QuantityInStock = 9850 },
                new AircraftPart { Id = 6, NIIN = "654987321", PartName = "Radar System", Manufacturer = "Raytheon", QuantityInStock = 5430 },
                new AircraftPart { Id = 7, NIIN = "147258369", PartName = "Fuel Injection Nozzle", Manufacturer = "General Electric", QuantityInStock = 22100 }
                );
        }

        
        

        
    }
}