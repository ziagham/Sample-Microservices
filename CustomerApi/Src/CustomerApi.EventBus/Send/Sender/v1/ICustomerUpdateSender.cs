using CustomerApi.Domain.AggregatesModel.CustomerAggregate;

namespace CustomerApi.EventBus.Send.Sender.v1
{
    public interface ICustomerUpdateSender
    {
        void SendCustomer(Customer customer);
    }
}