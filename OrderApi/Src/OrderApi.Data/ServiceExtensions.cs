using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using OrderApi.Data.Database.v1;

namespace OrderApi.Data.v1
{
    public static class ServiceExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<OrderContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("OrderDatabase"));
                });
                
                services.Migrate();
            }
            else
            {
                services.AddDbContext<OrderContext>(options => 
                {
                    options.UseInMemoryDatabase("OrderDb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
            }

            services.SeedData();
        }
    }
}
