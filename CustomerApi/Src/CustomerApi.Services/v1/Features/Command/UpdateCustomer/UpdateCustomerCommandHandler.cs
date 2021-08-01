using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.EventBus.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Services.v1.Features.Command.UpdateCustomer
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
            var updatedCustomer = new Customer {
                Id = request.Id, 
                FirstName = request.FirstName, 
                LastName = request.LastName, 
                Email = request.Email, 
                BirthDate = request.BirthDate
            };
            
            //Customer.UpdateCustomer(request.Id, request.FirstName, request.LastName, request.Email, request.BirthDate);
            var customer = await _customerRepository.UpdateAsync(updatedCustomer);

            _customerUpdateSender.SendCustomer(customer);

            return customer;
        }
    }
}