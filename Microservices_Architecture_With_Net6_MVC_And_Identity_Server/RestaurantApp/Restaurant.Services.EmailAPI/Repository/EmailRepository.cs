using Microsoft.EntityFrameworkCore;
using Restaurant.Services.EmailAPI.Infrastructure;
using Restaurant.Services.EmailAPI.Messages;
using Restaurant.Services.EmailAPI.Models;
namespace Restaurant.Services.EmailAPI.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> dbContext;

        public EmailRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            // Send Email
            //Create Log
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log= $"Order - {message.OrderId} has been created successfully"
            };

            await using var context = new ApplicationDbContext(dbContext);
            context.EmailLogs.Add(emailLog);
            await context.SaveChangesAsync();

        }
    }
}
