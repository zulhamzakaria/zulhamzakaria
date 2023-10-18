using Restaurant.MessageBus;

namespace Restaurant.Services.PaymentAPI.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        // This is the response message to the PaymentRequestMessage
        public int OrderId { get; set; }
        public bool Status { get; set; }

        // Populate email when the application sends out the result message
        public string Email { get; set; }
    }
}
