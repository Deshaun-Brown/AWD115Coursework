using Microsoft.EntityFrameworkCore;
using System;

namespace Quaterly_Sales_app.Models
{
    public class QuarterlySalesContext : DbContext
    {
        public QuarterlySalesContext(DbContextOptions<QuarterlySalesContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Sales> Sales { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Firstname = "Joyce",
                    Lastname = "Valdez",
                    DOB = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = 0
                }
            );
        }
    }
}
