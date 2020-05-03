namespace facade
{

    public enum TransactionResponseType
    {
        OK,
        DECLINED
    }
    public interface ITransactionController
    {
        ITransactionRequest TransactionRequest {get;set;}
        void Execute();
        
        TransactionResponseType GetApiResponse();
    }
}