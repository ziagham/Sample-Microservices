using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
using MediatR;

namespace CustomerApi.Service.v1.Command
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
            var customer = Customer.CreateCustomer(request.FirstName, request.LastName, request.Email, request.BirthDate, this._customerUniquenessChecker);

            return await _customerRepository.AddAsync(customer);
        }
    }
}