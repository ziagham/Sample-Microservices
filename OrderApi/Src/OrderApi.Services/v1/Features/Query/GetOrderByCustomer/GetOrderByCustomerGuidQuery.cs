using System.Collections.Generic;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using System;

namespace OrderApi.Service.v1.Query.GetOrderByCustomer
{
    public class GetOrderByCustomerGuidQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; set; }
    }
}