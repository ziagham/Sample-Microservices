using System;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Command.CreateOrder
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }
    }
}