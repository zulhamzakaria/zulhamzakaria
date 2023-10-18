using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Restaurant.Services.EmailAPI.Messages;
using Restaurant.Services.EmailAPI.Models;
using Restaurant.Services.EmailAPI.Repository;
using System.Text;

namespace Restaurant.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string emailSubscriptionName;
        private readonly string orderUpdatePaymentResultTopic;

        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;

        private readonly IConfiguration configuration;
        private readonly EmailRepository emailRepository;

        public AzureServiceBusConsumer(EmailRepository emailRepository, IConfiguration configuration)
        {
            this.emailRepository = emailRepository;
            this.configuration = configuration;
            serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            emailSubscriptionName = configuration.GetValue<string>("EmailSubscriptionName");

            orderUpdatePaymentResultTopic = configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            // Client for serviceBusProcessor 
            var client = new ServiceBusClient(serviceBusConnectionString);

            orderUpdatePaymentStatusProcessor = client.CreateProcessor(orderUpdatePaymentResultTopic, emailSubscriptionName);
        }

        // Start Service Bus Processor
        public async Task Start()
        {
            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += MessageServiceErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();
        }

        private Task MessageServiceErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage resultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);
            try
            {
                await emailRepository.SendAndLogEmail(resultMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
