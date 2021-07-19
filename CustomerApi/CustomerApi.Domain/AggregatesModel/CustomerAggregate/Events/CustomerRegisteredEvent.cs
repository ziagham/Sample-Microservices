using System;
using CustomerApi.Domain.SeekWork;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate.Events
{
    public class CustomerRegisteredEvent : DomainEventBase
    {
        public Guid CustomerId { get; }

        public CustomerRegisteredEvent(Guid customerId)
        {
            this.CustomerId = customerId;
        }
    }
}