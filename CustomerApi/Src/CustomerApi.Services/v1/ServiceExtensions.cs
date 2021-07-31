using Microsoft.Extensions.DependencyInjection;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Data.v1.Rules;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
using MediatR;
using System.Reflection;
using CustomerApi.Service.v1.Behaviors;

namespace CustomerApi.Service.v1
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Inject Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Inject Rules
            services.AddScoped<ICustomerUniquenessChecker, CustomerUniquenessChecker>();

            // Inject 3rd parties
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}
