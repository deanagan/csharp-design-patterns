
namespace ChainOfResponsibility
{
    public class AmexCardHandler : CreditCardHandlerBase
    {
        private IPaymentGateway paymentGateway;
        private const string AmexCardStartingNumber = "3";
        public AmexCardHandler(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public override bool IsCreditCardValid(ICreditCard card)
        {
            if (card.Number.StartsWith(AmexCardStartingNumber))
            {
                return paymentGateway.SubmitVerification(this, card);
            }

            return base.IsCreditCardValid(card);
        }

    }
}