using CustomerApi.Domain.SeekWork;
using System;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity
    {
        #region properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Active { get; set; }
        #endregion

        #region constructors
        // Empty constructor for EF
        public Customer() {}

        // public Customer(Guid id, string firstName, string lastName, string email, DateTime? birthDate)
        // {
        //     Id = id;
        //     FirstName = firstName;
        //     LastName = lastName;
        //     Email = email;
        //     BirthDate = birthDate;
        //     Active = true;
        // }
        #endregion

        //#region methods

        // public static Customer CreateCustomer(string firstName, string lastName, string email, DateTime birthDate, 
        // ICustomerUniquenessChecker customerUniquenessChecker = null)
        // {
        //     return Customer.CreateCustomer(Guid.NewGuid(),firstName, lastName, email, birthDate);
        // }

        // public static Customer CreateCustomer(Guid id, string firstName, string lastName, string email, DateTime birthDate, 
        // ICustomerUniquenessChecker customerUniquenessChecker = null)
        // {
        //     if (customerUniquenessChecker != null)
        //         CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));
            
        //     return new Customer(id, firstName, lastName, email, birthDate);
        // }

        // public static Customer UpdateCustomer(Guid id, string firstName, string lastName, string email, DateTime birthDate)
        // {
        //     return new Customer(id, firstName, lastName, email, birthDate);
        // }
        //#endregion
    }
}
