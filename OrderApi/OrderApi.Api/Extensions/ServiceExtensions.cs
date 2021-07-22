using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OrderApi.Data.Database;
using System;
using OrderApi.EventBus.Receive.Options.v1;
using OrderApi.EventBus.Receive.Receiver.v1;
using OrderApi.Services.v1.Services;

namespace OrderApi.Api.Extensions
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

        public static void AddHostedExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceClientSettingsConfig = configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

             if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<CustomerFullNameUpdateReceiver>();
            }
        }

        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<OrderContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("OrderDatabase"));
                });
            }
            else
            {
                services.AddDbContext<OrderContext>(options => {
                    options.UseInMemoryDatabase("OrderDb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
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