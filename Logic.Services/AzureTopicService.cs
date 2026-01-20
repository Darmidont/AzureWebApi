using Azure.Messaging.ServiceBus;
using Logic.Interfaces.Interfaces;

namespace Logic.Services
{
    public class AzureTopicService(IConfigurationService configurationService) : IAzureTopicService
    {
        private readonly IConfigurationService _configurationService = configurationService;

        public async Task SendMessageAsync(string jsonMessage, string topicName, string subscriptionName, CancellationToken cancellationToken)
        {
            await using var client = new ServiceBusClient(configurationService.GetServiceBusConnectionString());
            ServiceBusSender sender = client.CreateSender(topicName);

            ServiceBusMessage message = new ServiceBusMessage(jsonMessage)
            {
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(message, cancellationToken);
        }
    }
}
