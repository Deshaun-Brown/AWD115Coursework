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
                },
                new Employee
                {
                    EmployeeId = 2,
                    Firstname = "John",
                    Lastname = "Smith",
                    DOB = new DateTime(1975, 4, 15),
                    DateOfHire = new DateTime(2005, 6, 20),
                    ManagerId = 1 
                },
                new Employee
                {
                    EmployeeId = 3,
                    Firstname = "Jane",
                    Lastname = "Doe",
                    DOB = new DateTime(1982, 8, 22),
                    DateOfHire = new DateTime(2010, 3, 1),
                    ManagerId = 1 
                }
            );

            modelBuilder.Entity<Sales>().HasData(
                new Sales { SalesId = 1, Quarter = 1, Year = 2023, Amount = 12000.50m, EmployeeId = 2 },
                new Sales { SalesId = 2, Quarter = 1, Year = 2023, Amount = 15300.75m, EmployeeId = 3 },
                new Sales { SalesId = 3, Quarter = 2, Year = 2023, Amount = 18000.00m, EmployeeId = 2 }
            );
        }
    }
}
