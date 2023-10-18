using Microsoft.EntityFrameworkCore;
using Restaurant.Services.OrderAPI.Infrastructure;
using Restaurant.Services.OrderAPI.Models;

namespace Restaurant.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> dbContext;

        public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var db = new ApplicationDbContext(dbContext);
            db.OrderHeaders.Add(orderHeader);
            await db.SaveChangesAsync();
            return true;    

        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            await using var db = new ApplicationDbContext(dbContext);
            var orderHeader = await db.OrderHeaders.FirstOrDefaultAsync(h => h.OrderHeaderId == orderHeaderId);
            if(orderHeader != null)
            {
                orderHeader.PaymentStatus = paid;
                await db.SaveChangesAsync();
            }
        }
    }
}
