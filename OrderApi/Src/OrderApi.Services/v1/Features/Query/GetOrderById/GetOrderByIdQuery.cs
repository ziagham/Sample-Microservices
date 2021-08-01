using System;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Query.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }
    }
}