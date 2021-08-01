using System.Collections.Generic;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace CustomerApi.Services.v1.Features.Query.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}