using System;
using System.Linq;
using System.Collections.Generic;
using OrderApi.Data.Database.v1;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Data.Tests.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(OrderContext context)
        {
            if (context.Orders.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(OrderContext context)
        {
            var orders = new List<Order>()
            {
                new Order
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    CustomerFullName = "Amin Ziagham"
                },
                new Order
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    CustomerFullName = "Darth Vader"
                },
                new Order
                {
                    Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    OrderState = 2,
                    CustomerGuid = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    CustomerFullName = "Keanu Reeves"
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}