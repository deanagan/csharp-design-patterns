namespace Facade;

public interface IPaymentSettlementFacade
{
    PaymentResult ProcessPayment(PaymentRequest request);
}

