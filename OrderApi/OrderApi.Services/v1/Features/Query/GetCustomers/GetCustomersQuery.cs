using System.Collections.Generic;
using OrderApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace OrderApi.Service.v1.Query.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}