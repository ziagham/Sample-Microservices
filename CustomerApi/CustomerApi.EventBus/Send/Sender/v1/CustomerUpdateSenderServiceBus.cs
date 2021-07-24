using Azure.Messaging.ServiceBus;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.EventBus.Send.Options.v1;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System;

namespace CustomerApi.EventBus.Send.Sender.v1
{
    public class CustomerUpdateSenderServiceBus : ICustomerUpdateSender
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public CustomerUpdateSenderServiceBus(IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
        }

        public async void SendCustomer(Customer customer)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var sender = client.CreateSender(_queueName);

                var json = JsonSerializer.Serialize(customer);
                var message = new ServiceBusMessage(json);

                await sender.SendMessageAsync(message);
            }
        }
    }
}