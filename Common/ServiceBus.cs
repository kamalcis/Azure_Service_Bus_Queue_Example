using Azure.Messaging.ServiceBus;

namespace Azure_Service_Bus_Queue_Example.Common
{
    public class ServiceBus : Interfaces.IServiceBus
    {
        IConfiguration _configuration;
        public ServiceBus(IConfiguration configuration) { 
            _configuration = configuration;
        }
        public ServiceBusClient GetServiceBusClient()
        {
            string connectionString = _configuration.GetSection("ConnectionString:Url").Value.ToString();           
            ServiceBusClient client = new ServiceBusClient(connectionString);
            return client;
        }
    }
}
