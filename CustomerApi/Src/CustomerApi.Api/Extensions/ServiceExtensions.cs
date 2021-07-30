using CustomerApi.Data.Database;
using CustomerApi.EventBus.Send.Options.v1;
using CustomerApi.EventBus.Send.Sender.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CustomerApi.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo 
                    { 
                        Title = "Customer Api", 
                        Version = "v1",
                        Description = "A simple API to create or update customers",
                        Contact = new OpenApiContact
                        {
                            Name = "Amin Ziagham",
                            Email = "amin.ziagham@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/ziagham/")
                        }
                    });
                });
            });
        }

        public static void AddEventBusExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceClientSettingsConfig = configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            serviceClientSettingsConfig = configuration.GetSection("AzureServiceBus");
            services.Configure<AzureServiceBusConfiguration>(serviceClientSettingsConfig);

            bool.TryParse(configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);

            if (useRabbitMq)
            {
                services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSender>();
            }
            else
            {
                services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSenderServiceBus>();
            }
        }

        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<CustomerContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("CustomerDatabase"));
                }, ServiceLifetime.Singleton);
            }
            else
            {
                services.AddDbContext<CustomerContext>(options => 
                {
                    options.UseInMemoryDatabase("CustomerDb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }, ServiceLifetime.Singleton);

                // services.AddDbContext<CustomerContext>(options => {
                //     options.UseInMemoryDatabase("CustomerDb");
                //     options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                // });
            }
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }
    }
}