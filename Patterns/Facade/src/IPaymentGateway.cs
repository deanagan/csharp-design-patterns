namespace Facade;

public interface IPaymentGateway
{
    GatewayAuthorization AuthorizeAndCapture(
        string cardNumber,
        string cardExpiry,
        string cardCvc,
        decimal amount,
        string currency);
}

