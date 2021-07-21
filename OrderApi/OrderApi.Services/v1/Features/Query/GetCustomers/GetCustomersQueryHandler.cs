using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace OrderApi.Service.v1.Query.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_customerRepository.GetAll().ToList());
        }
    }
}