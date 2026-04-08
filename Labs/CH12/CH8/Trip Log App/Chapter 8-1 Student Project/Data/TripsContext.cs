using Chapter_8_1_Student_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Chapter_8_1_Student_Project.Data;

public class TripsContext : DbContext
{
    public TripsContext(DbContextOptions<TripsContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Destination> Destinations => Set<Destination>();
    public DbSet<Accommodation> Accommodations => Set<Accommodation>();
    public DbSet<Activity> Activities => Set<Activity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Foreign keys config to restrict deletion
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Destination)
            .WithMany(d => d.Trips)
            .HasForeignKey(t => t.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Accommodation)
            .WithMany(a => a.Trips)
            .HasForeignKey(t => t.AccommodationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Destination>().HasData(
            new Destination { DestinationId = 1, Name = "New York, NY" },
            new Destination { DestinationId = 2, Name = "Orlando, FL" },
            new Destination { DestinationId = 3, Name = "Las Vegas, NV" }
        );

        modelBuilder.Entity<Accommodation>().HasData(
            new Accommodation { AccommodationId = 1, Name = "Hotel" },
            new Accommodation { AccommodationId = 2, Name = "Resort" },
            new Accommodation { AccommodationId = 3, Name = "Motel" }
        );

        modelBuilder.Entity<Activity>().HasData(
            new Activity { ActivityId = 1, Name = "Museum" },
            new Activity { ActivityId = 2, Name = "Theme Park" },
            new Activity { ActivityId = 3, Name = "Casino" }
        );

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                TripId = 1,
                DestinationId = 1,
                AccommodationId = 1,
                StartDate = new DateTime(2026, 3, 10),
                EndDate = new DateTime(2026, 3, 15)
            },
            new Trip
            {
                TripId = 2,
                DestinationId = 2,
                AccommodationId = 2,
                StartDate = new DateTime(2026, 5, 1),
                EndDate = new DateTime(2026, 5, 5)
            },
            new Trip
            {
                TripId = 3,
                DestinationId = 3,
                AccommodationId = 3,
                StartDate = new DateTime(2026, 7, 10),
                EndDate = new DateTime(2026, 7, 20)
            });
    }
}
