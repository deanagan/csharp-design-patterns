

namespace BridgePattern
{
    public class PaypalPayment : IPayment
    {
        private IPaymentGateway _mPaymentGateway;

        public PaypalPayment(IPaymentGateway paymentGateway)
        {
            _mPaymentGateway = paymentGateway;
        }
        public void SubmitPayment(decimal amount)
        {
            _mPaymentGateway.ProcessPayment(amount, this);
        }
    }
}
