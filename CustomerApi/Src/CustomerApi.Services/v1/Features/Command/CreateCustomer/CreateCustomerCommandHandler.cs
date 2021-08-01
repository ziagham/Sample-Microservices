using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.EventBus.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Services.v1.Features.Command.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var createCustomer = new Customer {
                FirstName = request.FirstName, 
                LastName = request.LastName, 
                Email = request.Email, 
                BirthDate = request.BirthDate
            };

            var customer = await _customerRepository.AddAsync(createCustomer);
            
            return customer;
        }
    }
}