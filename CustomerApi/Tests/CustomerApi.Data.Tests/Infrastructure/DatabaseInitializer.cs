using System;
using System.Linq;
using System.Collections.Generic;
using CustomerApi.Data.Database.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;

namespace CustomerApi.Data.Tests.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(CustomerContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(CustomerContext context)
        {
            var customerEntities = new List<Customer>()
            {
                new Customer
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    FirstName = "Amin",
                    LastName = "Ziagham",
                    Email = "amin.ziagham@gmail.com",
                    BirthDate = new DateTime(1985, 04, 06),
                    Active = true
                },
                new Customer
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    FirstName = "Darth",
                    LastName = "Vader",
                    Email = "darth.vader@domain.com",
                    BirthDate = new DateTime(1977, 05, 25),
                    Active = true
                },
                new Customer
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    FirstName = "Keanu",
                    LastName = "Reeves",
                    Email = "keanu.reeves@domain.com",
                    BirthDate = new DateTime(1964, 09, 02),
                    Active = true
                }
            };

            context.Customers.AddRange(customerEntities);
            context.SaveChanges();
        }
    }
}