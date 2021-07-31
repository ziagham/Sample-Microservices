using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace OrderApi.Data.v1.Database
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
            return dbContext;
        }
    }
}