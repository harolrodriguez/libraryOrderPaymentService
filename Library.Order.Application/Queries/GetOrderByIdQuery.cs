using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Order.Application.Interfaces;
using Library.Order.Domain.Entities;

namespace Library.Order.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponseDto> { public Guid OrderId { get; set; } }
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; } public Guid UserId { get; set; } public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty; public decimal TotalAmount { get; set; }
        public string DeliveryType { get; set; } = string.Empty; public Guid? DeliveryAddressId { get; set; }
        public decimal DeliveryCost { get; set; } public Guid? PaymentId { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }
    public class OrderItemResponseDto { public Guid ProductId { get; set; } public int Quantity { get; set; } public decimal UnitPrice { get; set; } public decimal Subtotal { get; set; } }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository) { _orderRepository = orderRepository; }

        public async Task<OrderResponseDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null) return null;
            return new OrderResponseDto
            {
                OrderId = order.Id, UserId = order.UserId, OrderDate = order.OrderDate, Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount, DeliveryType = order.DeliveryType.ToString(), DeliveryAddressId = order.DeliveryAddressId,
                DeliveryCost = order.DeliveryCost, PaymentId = order.PaymentId,
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto { ProductId = oi.ProductId, Quantity = oi.Quantity, UnitPrice = oi.UnitPrice, Subtotal = oi.Subtotal }).ToList()
            };
        }
    }
}