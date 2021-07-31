using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.EventBus.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Service.v1.Command.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public UpdateCustomerCommandHandler(ICustomerUpdateSender customerUpdateSender, ICustomerRepository customerRepository)
        {
            _customerUpdateSender = customerUpdateSender;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var updatedCustomer = Customer.UpdateCustomer(request.Id, request.FirstName, request.LastName, request.Email, request.BirthDate);
            var customer = await _customerRepository.UpdateAsync(updatedCustomer);

            _customerUpdateSender.SendCustomer(customer);

            return customer;
        }
    }
}