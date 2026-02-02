using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" }
            );

            // Seed Contacts
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    Firstname = "Delores",
                    Lastname = "Del Rio",
                    Phone = "555-987-6543",
                    Email = "delores@hotmail.com",
                    CategoryId = 2,
                    DateAdded = new DateTime(2024, 1, 1)
                },
                new Contact
                {
                    ContactId = 2,
                    Firstname = "John",
                    Lastname = "Doe",
                    Phone = "555-123-4567",
                    Email = "john.doe@example.com",
                    CategoryId = 1,
                    DateAdded = new DateTime(2024, 1, 15)
                },
                new Contact
                {
                    ContactId = 3,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Phone = "555-234-5678",
                    Email = "jane.smith@company.com",
                    CategoryId = 3,
                    DateAdded = new DateTime(2024, 2, 1)
                }
            );
        }
    }
}