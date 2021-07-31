using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;

namespace CustomerApi.Data.v1.Database
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

            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.AddRange(customerEntities);
            }

            return dbContext;
        }
    }
}