using System;
using OrderApi.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace OrderApi.Service.v1.Command.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        // public Customer Customer { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public CreateCustomerCommand(string firstName, string lastName, string email, DateTime birthDate)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        } 
    }
}