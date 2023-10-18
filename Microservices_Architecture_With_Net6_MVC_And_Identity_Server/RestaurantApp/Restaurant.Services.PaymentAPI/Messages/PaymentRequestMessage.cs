using Restaurant.MessageBus;

namespace Restaurant.Services.PaymentAPI.Messages
{
    // This will be the posted message on the AzureServiceBus
    // Once processed by PaymentProcessor, a new message containing OrderId and Status will be posted
    // New message will be processed by OrderAPI and subsequently update the PaymentStatus
    public class PaymentRequestMessage : BaseMessage
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public double OrderTotal { get; set; }

        // Email
        public string Email { get; set; }
    }
}
