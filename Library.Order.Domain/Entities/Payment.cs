using Library.Order.Domain.Enums;
using System;
namespace Library.Order.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public PaymentMethod Method { get; private set; }
        public string TransactionId { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string? ReceiptUrl { get; private set; }
        public string? AdditionalDetails { get; private set; }
        private Payment() { } // Para EF Core
        public Payment(Guid orderId, PaymentMethod method, string transactionId, decimal amount, PaymentStatus status, string? receiptUrl = null, string? additionalDetails = null)
        { Id = Guid.NewGuid(); OrderId = orderId; Method = method; TransactionId = transactionId; Amount = amount; Status = status; PaymentDate = DateTime.UtcNow; ReceiptUrl = receiptUrl; AdditionalDetails = additionalDetails; }
        public void UpdateStatus(PaymentStatus newStatus, string? transactionId = null, string? additionalDetails = null)
        { if (Status != newStatus) { Status = newStatus; PaymentDate = DateTime.UtcNow; } if (transactionId != null && string.IsNullOrEmpty(TransactionId)) TransactionId = transactionId; if (additionalDetails != null) AdditionalDetails = additionalDetails; }
    }
}