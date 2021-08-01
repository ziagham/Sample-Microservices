using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            serviceClientSettingsConfig = configuration.GetSection("AzureServiceBus");
            services.Configure<AzureServiceBusConfiguration>(serviceClientSettingsConfig);

            services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

            bool.TryParse(configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);

            if (useRabbitMq)
            {
                services.AddHostedService<CustomerFullNameUpdateReceiver>();
            }
            else
            {
                services.AddHostedService<CustomerFullNameUpdateReceiverServiceBus>();
            }
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}