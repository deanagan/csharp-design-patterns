namespace facade
{
    public interface ITransactionRequest
    {
        decimal Amount{get;set;}
        IBillingAddress BillingAddress{get;set;}

        ICreditCard CreditCard {get;set;}

    }
}