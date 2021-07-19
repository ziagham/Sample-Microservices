using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Events;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
using CustomerApi.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity
    {
        #region properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Phone { get; private set; }
        public string Mobile { get; private set; }
        public bool IsActive { get; private set; }
        private bool WelcomeEmailWasSent;
        #endregion

        #region constructors
        // Empty constructor for EF
        private Customer() 
        {

        }

        // public Customer(Guid id, string firstName, string lastName, string email,
        //     DateTime? birthDate, string address, string city, string state, string postalCode,
        //     string phone, string mobile)

        private Customer(Guid id, string firstName, string lastName, string email,
            DateTime? birthDate, string address, string city, string state, string postalCode,
            string phone, string mobile)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Phone = phone;
            Mobile = mobile;
            
            this.AddDomainEvent(new CustomerRegisteredEvent(this.Id));
        }

        #endregion

        #region methods

        public static Customer CreateCustomer(string firstName, string lastName, string email, 
        ICustomerUniquenessChecker customerUniquenessChecker)
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));
            return new Customer(Guid.NewGuid(), firstName, lastName, email, null, null, null, null, null, null, null);
        }

        public void MarkAsWelcomedByEmail()
        {
            this.WelcomeEmailWasSent = true;
        }
        #endregion
    }
}
