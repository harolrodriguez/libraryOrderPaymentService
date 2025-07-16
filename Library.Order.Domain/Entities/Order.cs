using Library.Order.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Order.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DeliveryType DeliveryType { get; private set; }
        public Guid? DeliveryAddressId { get; private set; }
        public decimal DeliveryCost { get; private set; }
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Guid? PaymentId { get; private set; } // Referencia al pago principal
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Order() { } // Para EF Core
        public Order(Guid userId, DeliveryType deliveryType, Guid? deliveryAddressId, decimal deliveryCost, List<OrderItem> items)
        {
            Id = Guid.NewGuid(); UserId = userId; OrderDate = DateTime.UtcNow;
            Status = OrderStatus.PendientePago; DeliveryType = deliveryType;
            DeliveryAddressId = deliveryAddressId; DeliveryCost = deliveryCost;
            _orderItems.AddRange(items); CalculateTotalAmount();
            CreatedAt = DateTime.UtcNow; UpdatedAt = DateTime.UtcNow;
        }
        private void CalculateTotalAmount() { TotalAmount = _orderItems.Sum(item => item.Subtotal) + DeliveryCost; }
        public void UpdateStatus(OrderStatus newStatus) { if (Status != newStatus) { Status = newStatus; UpdatedAt = DateTime.UtcNow; } }
        public void SetPayment(Guid paymentId) { PaymentId = paymentId; UpdatedAt = DateTime.UtcNow; }
    }
}