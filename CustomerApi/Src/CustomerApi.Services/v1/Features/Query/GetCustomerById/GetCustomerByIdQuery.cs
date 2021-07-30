using System;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace CustomerApi.Service.v1.Query.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}