using MediatR;
using System.Collections.Generic;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Command.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public List<Order> Orders { get; set; }
    }
}