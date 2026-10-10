
namespace ChainOfResponsibility;

public class MastercardHandler(IPaymentGateway paymentGateway) : CreditCardHandlerBase
{
    private const string MasterCardStartingNumber = "5";

    public override bool IsCreditCardValid(ICreditCard card)
    {
        if (card.Number.StartsWith(MasterCardStartingNumber))
        {
            return paymentGateway.SubmitVerification(this, card);
        }

        return base.IsCreditCardValid(card);
    }

}