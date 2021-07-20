using System.Collections.Generic;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}