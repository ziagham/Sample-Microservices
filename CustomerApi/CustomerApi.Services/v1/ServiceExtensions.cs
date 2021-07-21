using Microsoft.Extensions.DependencyInjection;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Data.Rules.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
using MediatR;
using FluentValidation;
using System.Reflection;
using CustomerApi.Service.v1.Behaviors;

namespace CustomerApi.Service.v1
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Inject Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            // Inject Rules
            services.AddScoped<ICustomerUniquenessChecker, CustomerUniquenessChecker>();

            // Inject 3rd parties
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}
