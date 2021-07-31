using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using OrderApi.Data.v1.Repository;
using OrderApi.Service.v1.Behaviors;
using OrderApi.Services.v1.Services;

namespace OrderApi.Service.v1
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Inject Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();

            // Inject 3rd parties
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}
