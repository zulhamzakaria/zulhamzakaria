namespace Restaurant.Services.OrderAPI.Messages
{
    public class UpdatePaymentResultMessage
    {
        // This is the response message to the PaymentRequestMessage
        public int OrderId { get; set; }
        public bool Status { get; set; }

        //Order sends out Email
        public string Email { get; set; }
    }
}
