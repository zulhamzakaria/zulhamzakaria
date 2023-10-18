using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentProcessor;
using Restaurant.MessageBus;
using Restaurant.Services.PaymentAPI.Messages;
//using Restaurant.Services.PaymentAPI.Models;
//using Restaurant.Services.PaymentAPI.Repository;
using System.Text;

namespace Restaurant.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string paymentSubscriptionName;
        private readonly string paymentMessageTopic;
        private readonly string orderUpdatePaymentResultTopic;


        private ServiceBusProcessor paymentServiceBusProcessor;
        private readonly IProcessPayment processPayment;
        private readonly IConfiguration configuration;
        private readonly IMessageBus messageBus;

        public AzureServiceBusConsumer(IProcessPayment processPayment, IConfiguration configuration, IMessageBus messageBus)
        {
            this.processPayment = processPayment;
            this.configuration = configuration;
            this.messageBus = messageBus;

            serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            paymentSubscriptionName = configuration.GetValue<string>("PaymentSubscriptionName");
            paymentMessageTopic = configuration.GetValue<string>("PaymentMessageTopic");
            orderUpdatePaymentResultTopic = configuration.GetValue<string>("OrderUpdatePaymentResultTopic");


            // Client for serviceBusProcessor 
            var client = new ServiceBusClient(serviceBusConnectionString);
            paymentServiceBusProcessor = client.CreateProcessor(paymentMessageTopic, paymentSubscriptionName);
        }
        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {

            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            // Process payment
            var result = processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
                Email = paymentRequestMessage.Email,
            };

            //Public payment message
            try
            {
                await messageBus.PublishedMessage(updatePaymentResultMessage, orderUpdatePaymentResultTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Start Service Bus Processor
        public async Task Start()
        {
            // Receive message from the Service Bus
            // Populate the message event args and passes it into the paramater
            paymentServiceBusProcessor.ProcessMessageAsync += ProcessPayments;

            paymentServiceBusProcessor.ProcessErrorAsync += MessageServiceErrorHandler;

            await paymentServiceBusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await paymentServiceBusProcessor.StopProcessingAsync();
            await paymentServiceBusProcessor.DisposeAsync();
        }

        private Task MessageServiceErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

    }
}
