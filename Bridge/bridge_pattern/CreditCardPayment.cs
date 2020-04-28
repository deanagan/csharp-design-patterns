

namespace BridgePattern
{
    public class CreditCardPayment : IPayment
    {
        private IPaymentGateway _mPaymentGateway;

        public CreditCardPayment(IPaymentGateway paymentGateway)
        {
            _mPaymentGateway = paymentGateway;
        }
        public void SubmitPayment(decimal amount)
        {
            _mPaymentGateway.ProcessPayment(amount, this);
        }
    }
}
