using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;

namespace CustomerApi.Data.Database.v1
{
    public static class DataInitializer
    {
        public static void Migrate(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            using (var context = serviceProvider.GetService<CustomerContext>())
            {
                context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public static void SeedData(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            using (var context = serviceProvider.GetService<CustomerContext>())
            {
                context.AddOrUpdateSeedData();
                context.SaveChanges();
            }
        }

        public static IServiceScope CreateScope(this IServiceProvider provider)
        {
            return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        internal static CustomerContext AddOrUpdateSeedData(this CustomerContext dbContext)
        {
            var customerEntities = new List<Customer>()
            {
                new Customer
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    FirstName = "Amin",
                    LastName = "Ziagham",
                    Email = "amin.ziagham@gmail.com",
                    BirthDate = new DateTime(1985, 04, 06),
                    Active = true
                },
                new Customer
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    FirstName = "Darth",
                    LastName = "Vader",
                    Email = "darth.vader@domain.com",
                    BirthDate = new DateTime(1977, 05, 25),
                    Active = true
                },
                new Customer
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    FirstName = "Keanu",
                    LastName = "Reeves",
                    Email = "keanu.reeves@domain.com",
                    BirthDate = new DateTime(1964, 09, 02),
                    Active = true
                }
            };
            
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.AddRange(customerEntities);
            }

            return dbContext;
        }
    }
}