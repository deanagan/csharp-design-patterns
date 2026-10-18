namespace Facade;

public interface ILedgerService
{
    string RecordTransaction(string orderId, decimal amount, string currency, string authorizationCode);
}

