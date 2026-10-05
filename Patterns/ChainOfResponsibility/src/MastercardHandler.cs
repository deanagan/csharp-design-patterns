
namespace ChainOfResponsibility
{
    public class MastercardHandler : CreditCardHandlerBase
    {
        private IPaymentGateway paymentGateway;
        private const string MasterCardStartingNumber = "5";
        public MastercardHandler(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public override bool IsCreditCardValid(ICreditCard card)
        {
            if (card.Number.StartsWith(MasterCardStartingNumber))
            {
                return paymentGateway.SubmitVerification(this, card);
            }

            return base.IsCreditCardValid(card);
        }

    }
}