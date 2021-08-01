using System.Collections.Generic;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using System;

namespace OrderApi.Services.v1.Features.Query.GetOrderByCustomer
{
    public class GetOrderByCustomerGuidQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; set; }
    }
}