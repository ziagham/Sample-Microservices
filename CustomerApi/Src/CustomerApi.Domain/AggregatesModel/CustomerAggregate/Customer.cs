using CustomerApi.Domain.SeekWork;
using System;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Active { get; set; }

        public Customer() 
        {
            this.CreatedUtc = DateTime.Now;
        }
    }
}
