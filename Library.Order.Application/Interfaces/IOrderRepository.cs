using Library.Order.Domain.Entities;

namespace Library.Order.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<Domain.Entities.Order?> GetOrderByIdAsync(Guid orderId);
        Task AddOrderAsync(Domain.Entities.Order order);
        Task UpdateOrderAsync(Domain.Entities.Order order);

        Task AddPaymentAsync(Payment payment);
        IUnitOfWork UnitOfWork { get; }
    }
}