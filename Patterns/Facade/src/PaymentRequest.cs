namespace Facade;

public record PaymentRequest(
    string OrderId,
    string CustomerEmail,
    decimal Amount,
    string Currency,
    string CardNumber,
    string CardExpiry,
    string CardCvc);

