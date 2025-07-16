using MediatR;
using Library.Order.Application.Commands;
using Library.Order.Application.Interfaces;
using Library.Order.Domain.Entities;
using Library.Order.Domain.Enums;
using Library.Order.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Order.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IProductService _productService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IPaymentGatewayService paymentGatewayService, IProductService productService)
        {
            _orderRepository = orderRepository;
            _paymentGatewayService = paymentGatewayService;
            _productService = productService;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar Productos y Obtener Precios (Mock del Product Microservice)
            var productDetails = await _productService.GetProductDetails(request.Items.Select(i => i.ProductId).ToList());
            if (productDetails == null || productDetails.Count != request.Items.Count || productDetails.Any(p => p == null)) throw new ApplicationException("Uno o más productos no encontrados.");

            var orderItems = new List<OrderItem>();
            foreach (var itemDto in request.Items)
            {
                var product = productDetails.FirstOrDefault(p => p.ProductId == itemDto.ProductId);
                if (product == null || (product.IsPhysical && product.StockQuantity < itemDto.Quantity)) throw new ApplicationException($"Stock insuficiente para el producto {itemDto.ProductId}.");
                orderItems.Add(new OrderItem(Guid.NewGuid(), itemDto.ProductId, itemDto.Quantity, product.Price));
            }

            // 2. Calcular Costo de Delivery (lógica simulada)
            decimal deliveryCost = 0;
            if (request.DeliveryType == DeliveryType.DeliveryADomicilio)
            {
                deliveryCost = 15.00m; // Costo de ejemplo
                if (!request.DeliveryAddressId.HasValue || request.DeliveryAddressId == Guid.Empty) throw new ApplicationException("Se requiere una dirección de entrega para Delivery a Domicilio.");
            }

            // 3. Crear Orden
            var order = new Domain.Entities.Order(request.UserId, request.DeliveryType, request.DeliveryAddressId, deliveryCost, orderItems);

            // 4. Iniciar Pago con Gateway (Mock del Payment Microservice)
            // Nota: El pago se "inicia" aquí, pero la confirmación real vendría de un webhook.
            // Para este alcance simplificado, solo se registra el inicio del pago.
            var paymentResult = await _paymentGatewayService.InitiatePayment(order.Id, order.TotalAmount, request.PaymentMethod, order.UserId);

            // 5. Crear registro de Pago y vincularlo a la Orden
            var payment = new Payment(order.Id, request.PaymentMethod, paymentResult.TransactionId, order.TotalAmount, PaymentStatus.Pendiente, paymentResult.ReceiptUrl, paymentResult.Instructions);
            order.SetPayment(payment.Id); // Vincular el pago a la orden

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.AddPaymentAsync(payment); // Guardar el registro de pago inicial
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // 6. Retornar respuesta
            return new OrderResponse
            {
                OrderId = order.Id, TotalAmount = order.TotalAmount, Status = order.Status, PaymentMethod = request.PaymentMethod,
                PaymentInstructions = paymentResult.Instructions, RedirectUrl = paymentResult.RedirectUrl
            };
        }
    }
}