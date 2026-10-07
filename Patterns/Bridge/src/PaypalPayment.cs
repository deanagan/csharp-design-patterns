

namespace Bridge;

public class PaypalPayment(IPaymentGateway paymentGateway) : IPayment
{
    public void SubmitPayment(decimal amount)
    {
        paymentGateway.ProcessPayment(amount, this);
    }
}
