using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Restaurant.MessageBus;
using Restaurant.Services.OrderAPI.Messages;
using Restaurant.Services.OrderAPI.Models;
using Restaurant.Services.OrderAPI.Repository;
using System.Text;

namespace Restaurant.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string checkoutSubscriptionName;

        //// AzureServiceBus topic name
        //private readonly string checkoutMessageTopic;
        // Using Q
        private readonly string checkoutMessageQueue;

        private readonly string paymentMessageTopic;
        private readonly string orderUpdatePaymentResultTopic;


        private ServiceBusProcessor checkoutServiceBusProcessor;
        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;

        //private readonly IOrderRepository orderRepository;
        private readonly IConfiguration configuration;
        private readonly IMessageBus messageBus;
        private readonly OrderRepository orderRepository;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, IMessageBus messageBus)
        {
            this.orderRepository = orderRepository;
            this.configuration = configuration;
            this.messageBus = messageBus;
            serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutSubscriptionName = configuration.GetValue<string>("CheckoutSubscriptionName");

            // Using Q
            //checkoutMessageTopic = configuration.GetValue<string>("CheckoutMessageTopic");
            checkoutMessageQueue = configuration.GetValue<string>("CheckoutMessageQueue");

            paymentMessageTopic = configuration.GetValue<string>("PaymentMessageTopic");
            orderUpdatePaymentResultTopic = configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            // Client for serviceBusProcessor 
            var client = new ServiceBusClient(serviceBusConnectionString);

            // Using Q
            //checkoutServiceBusProcessor = client.CreateProcessor(checkoutMessageTopic, checkoutSubscriptionName);
            checkoutServiceBusProcessor = client.CreateProcessor(checkoutMessageQueue);


            orderUpdatePaymentStatusProcessor = client.CreateProcessor(orderUpdatePaymentResultTopic, checkoutSubscriptionName);
        }
        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            // Message sent to the Azure Service Bus from Restaurant.MessageBus AzureServiceBusMessageBus
            var message = args.Message;

            // Deserialize the message to CheckoutCartDTO
            // Refer to: CartAPIController Checkout()
            // Message --> CheckoutHeaderDTO --> OrderHeader, OrderDetails 
            var body = Encoding.UTF8.GetString(message.Body);

            // Deserialize body to CheckoutHeaderDTO
            CheckoutHeaderDTO checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDTO.UserId,
                FirstName = checkoutHeaderDTO.FirstName,
                LastName = checkoutHeaderDTO.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDTO.CardNumber,
                CouponCode = checkoutHeaderDTO.CouponCode,
                CVV = checkoutHeaderDTO.CVV,
                DiscountAmount = checkoutHeaderDTO.DiscountAmount,
                Email = checkoutHeaderDTO.Email,
                ExpiryMonthYear = checkoutHeaderDTO.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDTO.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDTO.Phone,
                PickupDateTime = checkoutHeaderDTO.PickupDateTime,
            };

            // Create OrderDetails
            // Maybe many
            foreach (var details in checkoutHeaderDTO.CartDetailsDTO)
            {
                // Assign properties
                OrderDetails orderDetails = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count
                };
                orderHeader.CartTotalItems += details.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await orderRepository.AddOrder(orderHeader);

            // PaymentRequestMessage
            PaymentRequestMessage paymentRequestMessage = new PaymentRequestMessage()
            {
                Name = $"{orderHeader.FirstName} {orderHeader.LastName}",
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.Email,
            };
            //Public payment message
            try
            {
                await messageBus.PublishedMessage(paymentRequestMessage, paymentMessageTopic);
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
            checkoutServiceBusProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkoutServiceBusProcessor.ProcessErrorAsync += MessageServiceErrorHandler;
            await checkoutServiceBusProcessor.StartProcessingAsync();

            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += MessageServiceErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await checkoutServiceBusProcessor.StopProcessingAsync();
            await checkoutServiceBusProcessor.DisposeAsync();

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
            // Message sent to the Azure Service Bus from Restaurant.MessageBus AzureServiceBusMessageBus
            var message = args.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            // Deserialize body to CheckoutHeaderDTO
            CheckoutHeaderDTO checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            //Message model
            UpdatePaymentResultMessage updatePaymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            await orderRepository.UpdateOrderPaymentStatus(updatePaymentResultMessage.OrderId,updatePaymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);

        }

    }
}
