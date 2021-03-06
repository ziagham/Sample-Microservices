using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CustomerApi.Data.Database.v1;

namespace CustomerApi.Data.v1
{
    public static class ServiceExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<CustomerContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("CustomerDatabase"));
                });
                
                services.Migrate();
            }
            else
            {
                services.AddDbContext<CustomerContext>(options => 
                {
                    options.UseInMemoryDatabase("CustomerDb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
            }

            services.SeedData();
        }
    }
}
