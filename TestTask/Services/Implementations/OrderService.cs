using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Order> GetOrder()
        {
            return await _applicationDbContext.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.Price * o.Quantity)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _applicationDbContext.Set<Order>()
                .Include(o => o.User)
            .Where(x => x.Quantity > 10)
            .ToListAsync();
        }
    }
}
