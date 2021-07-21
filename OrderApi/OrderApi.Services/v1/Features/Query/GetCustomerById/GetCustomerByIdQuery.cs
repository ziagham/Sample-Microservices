using System;
using OrderApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace OrderApi.Service.v1.Query.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}