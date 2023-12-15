using Azure.Messaging.ServiceBus;

namespace Azure_Service_Bus_Queue_Example.Interfaces
{
    public interface IServiceBus
    {
        public ServiceBusClient GetServiceBusClient();
    }
}
