namespace ChainOfResponsibility
{
    public abstract class CreditCardHandlerBase : ICreditCardHandler
    {
        private ICreditCardHandler nextCreditCardHandler;
        public ICreditCardHandler SetNext(ICreditCardHandler creditCardHandler)
        {
            nextCreditCardHandler = creditCardHandler;
            return creditCardHandler;
        }

        public virtual bool IsCreditCardValid(ICreditCard card)
        {
            if (nextCreditCardHandler != null)
            {
                return nextCreditCardHandler.IsCreditCardValid(card);
            }

            return false;
        }
    }
}
