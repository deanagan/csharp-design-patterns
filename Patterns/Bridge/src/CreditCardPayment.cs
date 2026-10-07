

namespace Bridge;

public class CreditCardPayment(IPaymentGateway paymentGateway) : IPayment
{
    public void SubmitPayment(decimal amount)
    {
        paymentGateway.ProcessPayment(amount, this);
    }
}
