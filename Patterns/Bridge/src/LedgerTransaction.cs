namespace Bridge;

public record LedgerTransaction(
    string ReferenceId,
    decimal Amount,
    string DebitAccount,
    string CreditAccount);

