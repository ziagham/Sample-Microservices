using Microsoft.Extensions.DependencyInjection;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Data.Rules.v1;
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
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerUniquenessChecker, CustomerUniquenessChecker>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
