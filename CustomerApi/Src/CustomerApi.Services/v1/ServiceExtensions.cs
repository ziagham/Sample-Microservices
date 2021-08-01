using Microsoft.Extensions.DependencyInjection;
using CustomerApi.Data.Repository.v1;
using MediatR;
using System.Reflection;
using CustomerApi.Services.v1.Behaviors;

namespace CustomerApi.Services.v1
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Inject Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Inject 3rd parties
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}
