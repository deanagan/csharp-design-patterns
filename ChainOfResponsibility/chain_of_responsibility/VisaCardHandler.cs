
namespace ChainOfResponsibility
{
    public class VisaCardHandler : CreditCardHandlerBase
    {
        private IPaymentGateway paymentGateway;
        private const string VisaCardStartingNumber = "4";
        public VisaCardHandler(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public override bool IsCreditCardValid(ICreditCard card)
        {
            if (card.Number.StartsWith(VisaCardStartingNumber))
            {
                return paymentGateway.SubmitVerification(this, card);
            }

            return base.IsCreditCardValid(card);
        }

    }
}