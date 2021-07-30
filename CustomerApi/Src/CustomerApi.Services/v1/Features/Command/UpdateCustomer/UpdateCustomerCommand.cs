using System;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace CustomerApi.Service.v1.Command.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        // public Customer Customer { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public UpdateCustomerCommand(Guid id, string firstName, string lastName, string email, DateTime birthDate)
        {
            this.Id = id;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        } 
    }
}