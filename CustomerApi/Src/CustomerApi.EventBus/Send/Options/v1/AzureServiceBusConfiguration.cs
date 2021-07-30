namespace CustomerApi.EventBus.Send.Options.v1
{
    public class AzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}