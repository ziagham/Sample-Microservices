using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
using CustomerApi.EventBus.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Service.v1.Command.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUniquenessChecker _customerUniquenessChecker;
        
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, 
                                            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            _customerRepository = customerRepository;
            _customerUniquenessChecker = customerUniquenessChecker;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var createCustomer = Customer.CreateCustomer(request.FirstName, request.LastName, request.Email, request.BirthDate, this._customerUniquenessChecker);
            var customer = await _customerRepository.AddAsync(createCustomer);
            
            return customer;
        }
    }
}