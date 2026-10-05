namespace ChainOfResponsibility
{
    public interface IPaymentGateway
    {
        bool SubmitVerification(ICreditCardHandler creditCardHandler, ICreditCard card);
    }
}