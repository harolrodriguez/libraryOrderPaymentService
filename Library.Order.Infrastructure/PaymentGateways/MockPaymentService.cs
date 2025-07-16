using Library.Order.Application.Interfaces;
using Library.Order.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Library.Order.Infrastructure.PaymentGateways
{
    public class MockPaymentService : IPaymentGatewayService
    {
        public Task<PaymentGatewayResult> InitiatePayment(Guid orderId, decimal amount, PaymentMethod method, Guid userId)
        {
            string transactionId = $"TXN_{Guid.NewGuid().ToString().Replace("-", "")}";
            string instructions = $"Instrucciones simuladas para {method}: Pagar S/.{amount:N2} (Transacción: {transactionId}).";
            string? redirectUrl = (method == PaymentMethod.VisaNiubiz) ? $"http://mock-niubiz.com/pay?order={orderId}&amount={amount}" : null;
            Console.WriteLine($"Simulando inicio de pago para Orden {orderId} con {method}. Monto: {amount}. Txn: {transactionId}");
            return Task.FromResult(new PaymentGatewayResult { TransactionId = transactionId, Instructions = instructions, RedirectUrl = redirectUrl, IsSuccess = true });
        }
    }

    public class PaymentGatewayResult
    {
        public string TransactionId { get; set; }
        public string Instructions { get; set; }
        public string? RedirectUrl { get; set; }
        public bool IsSuccess { get; set; }
    }

    public interface IPaymentGatewayService
    {
    }
}