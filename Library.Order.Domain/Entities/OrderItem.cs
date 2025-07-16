using System;
namespace Library.Order.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal { get; private set; }
        private OrderItem() { } // Para EF Core
        public OrderItem(Guid id, Guid productId, int quantity, decimal unitPrice)
        { Id = id; ProductId = productId; Quantity = quantity; UnitPrice = unitPrice; Subtotal = quantity * unitPrice; }
    }
}