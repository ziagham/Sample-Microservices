using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace CustomerApi.Services.v1.Features.Query.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);
        }
    }
}