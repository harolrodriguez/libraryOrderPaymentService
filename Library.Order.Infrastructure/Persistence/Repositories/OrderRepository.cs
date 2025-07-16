using Library.Order.Application.Interfaces;
using Library.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Order.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) { _context = context; }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.Entities.Order?> GetOrderByIdAsync(Guid orderId) { return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId); }
        public async Task AddOrderAsync(Domain.Entities.Order order) { await _context.Orders.AddAsync(order); }
        public Task UpdateOrderAsync(Domain.Entities.Order order) { _context.Orders.Update(order); return Task.CompletedTask; }

        public async Task AddPaymentAsync(Payment payment) { await _context.Payments.AddAsync(payment); }
    }
}