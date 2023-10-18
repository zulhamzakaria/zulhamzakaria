using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        // Connection string gotten from Azure Service Bus RootManageSharedAccessKey
        private readonly string connectionString = "";
        public async Task PublishedMessage(BaseMessage baseMessage, string topicName)
        {
            //ISenderClient senderClient = new TopicClient(connectionString, topicName);

            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender serviceBusSender = client.CreateSender(topicName);

            var message = JsonConvert.SerializeObject(baseMessage);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(message))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await serviceBusSender.SendMessageAsync(finalMessage);

            await serviceBusSender.DisposeAsync();
        }
    }
}
