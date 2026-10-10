
namespace ChainOfResponsibility;

public class AmexCardHandler(IPaymentGateway paymentGateway) : CreditCardHandlerBase
{
    private const string AmexCardStartingNumber = "3";

    public override bool IsCreditCardValid(ICreditCard card)
    {
        if (card.Number.StartsWith(AmexCardStartingNumber))
        {
            return paymentGateway.SubmitVerification(this, card);
        }

        return base.IsCreditCardValid(card);
    }
}
