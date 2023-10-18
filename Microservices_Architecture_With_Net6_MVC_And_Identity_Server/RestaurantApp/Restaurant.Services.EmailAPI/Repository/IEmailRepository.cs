using Restaurant.Services.EmailAPI.Messages;
using Restaurant.Services.EmailAPI.Models;

namespace Restaurant.Services.EmailAPI.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
