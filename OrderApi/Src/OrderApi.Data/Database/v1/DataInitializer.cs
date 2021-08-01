using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Data.Database.v1
{
    public static class DataInitializer
    {
        public static void Migrate(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            using (var context = serviceProvider.GetService<OrderContext>())
            {
                context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public static void SeedData(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            using (var context = serviceProvider.GetService<OrderContext>())
            {
                context.AddOrUpdateSeedData();
                context.SaveChanges();
            }
        }

        public static IServiceScope CreateScope(this IServiceProvider provider)
        {
            return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        internal static OrderContext AddOrUpdateSeedData(this OrderContext dbContext)
        {
            var orderEntities = new List<Order>()
            {
                new Order
                {
                    Id = Guid.Parse("c34a25d8-e786-4e00-9b70-6acf2e6187ac"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    CustomerFullName = "Amin Ziagham"
                },
                new Order
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    CustomerFullName = "Darth Vader"
                },
                new Order
                {
                    Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    OrderState = 2,
                    CustomerGuid = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    CustomerFullName = "Keanu Reeves"
                }
            };

            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.AddRange(orderEntities);
            }

            return dbContext;
        }
    }
}