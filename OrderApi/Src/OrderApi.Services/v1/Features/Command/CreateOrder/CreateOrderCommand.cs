using System;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Service.v1.Command.CreateOrder
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }
    }
}