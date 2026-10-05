
namespace ChainOfResponsibility
{
    public interface ICreditCardHandler
    {
        ICreditCardHandler SetNext(ICreditCardHandler creditCardHandler);
        bool IsCreditCardValid(ICreditCard card);
    }
}