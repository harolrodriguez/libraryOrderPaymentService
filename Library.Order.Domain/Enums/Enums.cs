namespace Library.Order.Domain.Enums
{
    public enum OrderStatus { PendientePago, Pagado, ProcesandoEnvio, Enviado, Entregado, Recogido, Cancelado }
    public enum PaymentMethod { Yape, TransferenciaBancaria, VisaNiubiz, PagoEfectivo }
    public enum PaymentStatus { Pendiente, Completado, Fallido, Reembolsado }
    public enum DeliveryType { RecojoEnSede, DeliveryADomicilio }
}