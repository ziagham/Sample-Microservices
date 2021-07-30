using System;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Database
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
            var customers = new[]
            {
                Customer.CreateCustomer(
                    Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    "Amin",
                    "Ziagham",
                    "amin.ziagham@gmail.com",
                    new DateTime(1985, 04, 06)
                ),
                Customer.CreateCustomer(
                    Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    "Darth",
                    "Vader",
                    "darth.vader@domain.com",
                     new DateTime(1977, 05, 25)
                ),
                Customer.CreateCustomer(
                    Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    "Keanu",
                    "Reeves",
                    "keanu.reeves@domain.com",
                    new DateTime(1964, 09, 02)
                ),
            };

            Customers.AddRange(customers);
            SaveChanges();
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.BirthDate).HasColumnType("date");
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });
        }
    }
}