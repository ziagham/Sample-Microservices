using Azure.Messaging.ServiceBus;
using OrderApi.EventBus.Receive.Options.v1;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OrderApi.Services.v1.Services;
using OrderApi.Services.v1.Models;
using System.Text;
using System;

namespace OrderApi.EventBus.Receive.Receiver.v1
{
    public class CustomerFullNameUpdateReceiverServiceBus : BackgroundService
    {
        private readonly ICustomerNameUpdateService _customerNameUpdateService;
        private readonly string _connectionString;
        private readonly string _queueName;
        private ServiceBusClient client;
        private ServiceBusProcessor processor;

        public CustomerFullNameUpdateReceiverServiceBus(ICustomerNameUpdateService customerNameUpdateService, IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
            _customerNameUpdateService = customerNameUpdateService;
            InitializeServiceBusListener();
        }

        private void InitializeServiceBusListener()
        {
            client = new ServiceBusClient(_connectionString);
            processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            processor.StartProcessingAsync();
            return Task.CompletedTask;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var updateCustomerFullNameModel = JsonSerializer.Deserialize<UpdateCustomerFullNameModel>(body);
            HandleMessage(updateCustomerFullNameModel);
            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private void HandleMessage(UpdateCustomerFullNameModel updateCustomerFullNameModel)
        {
            _customerNameUpdateService.UpdateCustomerNameInOrders(updateCustomerFullNameModel);
        }

        public async override void Dispose()
        {
            await processor.StopProcessingAsync();
            base.Dispose();
        }
    }
}