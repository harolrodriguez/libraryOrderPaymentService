using Library.Order.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Library.Order.Application.Interfaces
{
    public interface IPaymentGatewayService { Task<PaymentGatewayResult> InitiatePayment(Guid orderId, decimal amount, PaymentMethod method, Guid userId); }

    public class PaymentGatewayResult
    {
        public string TransactionId { get; set; } = string.Empty; 
        public string Instructions { get; set; } = string.Empty; 
        public string? RedirectUrl { get; set; } public bool IsSuccess { get; set; }
        public string? ReceiptUrl { get; set; }
    }
}