using Library.Order.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public DeliveryType DeliveryType { get; set; }
        public Guid? DeliveryAddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid OrderId { get; set; }
    }
    public class OrderItemDto { public Guid ProductId { get; set; } public int Quantity { get; set; } }
    public class OrderResponse
    {
        public Guid OrderId { get; set; } public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentInstructions { get; set; } public string? RedirectUrl { get; set; }
    }
}