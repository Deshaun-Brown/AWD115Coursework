using Chapter_8_1_Student_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Chapter_8_1_Student_Project.Data;

public class TripsContext : DbContext
{
    public TripsContext(DbContextOptions<TripsContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips => Set<Trip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                TripId = 1,
                Destination = "New York, NY",
                Accommodation = "Hotel",
                StartDate = new DateTime(2026, 3, 10),
                EndDate = new DateTime(2026, 3, 15),
                Activity1 = "Museum"
            },
            new Trip
            {
                TripId = 2,
                Destination = "Orlando, FL",
                Accommodation = "Resort",
                StartDate = new DateTime(2026, 5, 1),
                EndDate = new DateTime(2026, 5, 5),
                Activity1 = "Theme Park"
            });
    }
}
